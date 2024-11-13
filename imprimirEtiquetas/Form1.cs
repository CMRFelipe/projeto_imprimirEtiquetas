using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;

namespace imprimirEtiquetas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void main(int de, int ate)
        {
            string texto = "";
            string imprimir = "";
            if (checkBox1.Checked)
            {                
                while (de <= ate)
                {            
                    
                    texto = Convert.ToString(de) + "Q";
                    string barcodeData = "%YYY" + Convert.ToString(de) + "Q";
                    int xPosition = 140; // x = 2,000cm
                    int yPosition = 092;
                    int height = 105;
                    int width = 4;

                    int xPositionText = 250; // x = 2,000cm
                    int yPositionText = 280;
                    int fontSizeText = 140; // Tamanho da fonte do texto
                    int widthText = 500;

                    string zplCommand = "^XA" +
                                    $"^FO{xPosition},{yPosition}^BY{width}, {height}" + // Definir a largura da barra
                                    "^BCA,150,N,N,N" + // Configuração do código de barras Code 128-A
                                    "^FD" + barcodeData + "^FS" + // Dados do código de barras
                                    $"^FO{xPositionText},{yPositionText}^A0N,{fontSizeText},{fontSizeText}^FB{widthText},,0,C,0^FD{texto}^FS" +
                                    //$"^FO{xPositiontxt},{yPositiontxt}^A0N,250,40^FD" + texto + "^FS" + // Dados do texto
                                    "^XZ";

                    string printerIpAddress = "10.40.2.104"; // Substitua pelo endereço IP da impressora Zebra na rede

                    try
                    {
                        using (TcpClient client = new TcpClient(printerIpAddress, 9100))
                        {
                            using (NetworkStream stream = client.GetStream())
                            {
                                byte[] buffer = Encoding.ASCII.GetBytes(zplCommand);
                                stream.Write(buffer, 0, buffer.Length);
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao enviar o comando ZPL: {ex.Message}");
                    }
                    de = de + 1;
                }
                MessageBox.Show("Comando ZPL enviado com sucesso!");
            }
        
            else
            {

                while (de <= ate)
                {
                    texto = Convert.ToString(de);
                    string barcodeData = "%YYY" + Convert.ToString(de);
                    int xPosition = 140; // x = 2,000cm
                    int yPosition = 092;
                    int height = 105;
                    int width = 4;

                    int xPositionText = 130; // x = 2,000cm
                    int yPositionText = 280;
                    int fontSizeText = 140; // Tamanho da fonte do texto
                    int widthText = 500;
                    string zplCommand = "^XA" +
                                    $"^FO{xPosition},{yPosition}^BY{width}, {height}" + // Definir a largura da barra
                                    "^BCA,150,N,N,N" + // Configuração do código de barras Code 128-A
                                    "^FD" + barcodeData + "^FS" + // Dados do código de barras
                                    $"^FO{xPositionText},{yPositionText}^A0N,{fontSizeText},{fontSizeText}^FB{widthText},,0,C,0^FD{texto}^FS" +
                                    //$"^FO{xPositiontxt},{yPositiontxt}^A0N,250,40^FD" + texto + "^FS" + // Dados do texto
                                    "^XZ";

                    string printerIpAddress = "10.40.2.104"; // Substitua pelo endereço IP da impressora Zebra na rede

                    try
                    {
                        using (TcpClient client = new TcpClient(printerIpAddress, 9100))
                        {
                            using (NetworkStream stream = client.GetStream())
                            {
                                byte[] buffer = Encoding.ASCII.GetBytes(zplCommand);
                                stream.Write(buffer, 0, buffer.Length);
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao enviar o comando ZPL: {ex.Message}");
                    }
                    de = de + 1;
                }
                MessageBox.Show("Comando ZPL enviado com sucesso!");
            }            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int de = Convert.ToInt32(textBox1.Text);
            int ate = Convert.ToInt32(Convert.ToInt32(textBox2.Text));

            main(de, ate);
        }
    }
}
