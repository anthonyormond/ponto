using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ControledePonto
{
    public partial class FormPonto : Form
    {

        private TimeSpan JornadaPadrao = new TimeSpan(8, 30, 0); 


        public FormPonto()
        {
            InitializeComponent();

            textBoxSite.Text = "https://rhid.com.br/v2/#/login";
            textBox1.ScrollBars = ScrollBars.Vertical;
            maskedTextBoxCargaHoraria.Mask = "00:00:00";
            maskedTextBoxCargaHoraria.ValidatingType = typeof(DateTime);

            maskedTextBoxCargaHoraria.Text = "08:30:00"; // Carga horária padrão inicial

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Ponto(sender, e);

        }

        private void Ponto(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(maskedTextBoxCargaHoraria.Text))
            {
                MessageBox.Show("Por favor, informe a carga horária padrão.");
                return;
            }

            textBox1.Clear();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivos TXT|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var registros = new List<DateTime>();

                foreach (var linha in File.ReadLines(ofd.FileName))
                {
                    if (DateTime.TryParseExact(linha.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataHora))
                    {
                        registros.Add(dataHora);
                    }
                }

                var resultados = ProcessarRegistros(registros);

                var txtResultado = "";

                foreach (var r in resultados)
                {
                    txtResultado += ($"Data: {r.Data:dd/MM/yyyy} | Trabalhadas: {r.HorasTrabalhadas} | Extras: {r.HorasExtras} | Negativas: {r.HorasNegativas}\r\n");
                }

                var totalExtras = resultados.Sum(r => r.HorasExtras.TotalMinutes);
                var totalNegativas = resultados.Sum(r => r.HorasNegativas.TotalMinutes);

                txtResultado += ("\r\n");
                txtResultado += ($"Total de horas extras: {TimeSpan.FromMinutes(totalExtras)}\r\n");
                txtResultado += ($"Total de horas negativas: {TimeSpan.FromMinutes(totalNegativas)}\r\n");

                textBox1.Text = txtResultado;
            }
        }

        private List<ResultadoDia> ProcessarRegistros(List<DateTime> registros)
        {
            var agrupadoPorDia = registros
                .GroupBy(r => r.Date)
                .ToDictionary(g => g.Key, g => g.OrderBy(r => r).ToList());

            DateTime _data = DateTime.Parse(maskedTextBoxCargaHoraria.Text);

            JornadaPadrao = new TimeSpan(_data.Hour, _data.Minute, _data.Second );

            var resultados = new List<ResultadoDia>();

            foreach (var dia in agrupadoPorDia)
            {
                var data = dia.Key;
                var pontos = dia.Value;

                TimeSpan trabalhadas = TimeSpan.Zero;
                if (pontos.Count == 4)
                {
                    trabalhadas = (pontos[1] - pontos[0]) + (pontos[3] - pontos[2]);
                }
                else if (pontos.Count == 3)
                {
                    trabalhadas = (pontos[1] - pontos[0]) + (pontos[2] - (pontos[1] + TimeSpan.FromMinutes(30))); // assume 30 min almoço
                }
                else
                {
                    continue;
                }

                var extras = trabalhadas > JornadaPadrao ? trabalhadas - JornadaPadrao : TimeSpan.Zero;
                var negativas = trabalhadas < JornadaPadrao ? JornadaPadrao - trabalhadas : TimeSpan.Zero;

                resultados.Add(new ResultadoDia
                {
                    Data = data,
                    HorasTrabalhadas = trabalhadas,
                    HorasExtras = extras,
                    HorasNegativas = negativas
                });
            }

            return resultados;
        }

        private void ButtonAcessar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(maskedTextBoxCargaHoraria.Text))
            {
                MessageBox.Show("Por favor, informe a carga horária padrão.");
                return;
            }

            textBox1.Clear();

            var options = new ChromeOptions();
            // options.AddArgument("--headless"); // se quiser rodar sem abrir a janela
            options.AddArgument("--headless=new"); // Executa em background (modo invisível)
            options.AddArgument("--incognito");
            options.AddArgument("--disable-gpu");

            using (IWebDriver driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(textBoxSite.Text);

                // Aguarda a página carregar
                Thread.Sleep(2000);

                // Insere usuário e senha (verifique os IDs reais dos campos)
                driver.FindElement(By.Id("email")).SendKeys(textBoxLogin.Text);
                driver.FindElement(By.Id("password")).SendKeys(textBoxSenha.Text);

                driver.FindElement(By.Id("m_login_signin_submit")).Click();

                Thread.Sleep(5000); // Espera login completar

                // Navega para a página de apuração
                driver.Navigate().GoToUrl("https://rhid.com.br/v2/#/comprovantes_marcacao");

                Thread.Sleep(9000);

                driver.FindElement(By.Id("n_1")).SendKeys(dateTimePickerInicio.Text);

                try
                {
                    driver.FindElement(By.Id("n_2")).SendKeys(dateTimePickerFim.Text);
                }
                catch (Exception)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('n_2').value = arguments[0];", dateTimePickerFim.Text);
                }

                driver.FindElement(By.Id("btnCalcula")).Click();

                Thread.Sleep(5000);

                var linhas = driver.FindElements(By.CssSelector("table.cid_datatable tbody tr"));

                var listaHorarios = new List<string>();

                var registros = new List<DateTime>();

                foreach (var linha in linhas)
                {
                    var colunas = linha.FindElements(By.TagName("td"));
                    if (colunas.Count >= 3)
                    {
                        string teste = (colunas[2].Text.Trim());

                        if (DateTime.TryParseExact(colunas[2].Text.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataHora))
                        {
                            registros.Add(dataHora);
                        }

                    }
                }

                var resultados = ProcessarRegistros(registros);

                var txtResultado = "";

                foreach (var r in resultados)
                {
                    txtResultado += ($"Data: {r.Data:dd/MM/yyyy} | Trabalhadas: {r.HorasTrabalhadas} | Extras: {r.HorasExtras} | Negativas: {r.HorasNegativas}\r\n");
                }

                var totalExtras = resultados.Sum(r => r.HorasExtras.TotalMinutes);
                var totalNegativas = resultados.Sum(r => r.HorasNegativas.TotalMinutes);

                txtResultado += ("\r\n");
                txtResultado += ($"Total de horas extras: {TimeSpan.FromMinutes(totalExtras)}\r\n");
                txtResultado += ($"Total de horas negativas: {TimeSpan.FromMinutes(totalNegativas)}\r\n");

                textBox1.Text = txtResultado;
                
                driver.Quit();
            }
        }
    }

    class ResultadoDia
    {
        public DateTime Data { get; set; }
        public TimeSpan HorasTrabalhadas { get; set; }
        public TimeSpan HorasExtras { get; set; }
        public TimeSpan HorasNegativas { get; set; }
    }


}
