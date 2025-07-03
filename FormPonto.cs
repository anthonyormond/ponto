using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControledePonto
{
    public partial class FormPonto : Form
    {

        static readonly TimeSpan JornadaPadrao = new TimeSpan(9, 30, 0); // 9h30min
        
        public FormPonto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Ponto(sender, e);

        }

        private void Ponto(object sender, EventArgs e)
        {
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
                    txtResultado +=  ($"Data: {r.Data:dd/MM/yyyy} | Trabalhadas: {r.HorasTrabalhadas} | Extras: {r.HorasExtras} | Negativas: {r.HorasNegativas}\r\n");
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



    }

    class ResultadoDia
    {
        public DateTime Data { get; set; }
        public TimeSpan HorasTrabalhadas { get; set; }
        public TimeSpan HorasExtras { get; set; }
        public TimeSpan HorasNegativas { get; set; }
    }


}
