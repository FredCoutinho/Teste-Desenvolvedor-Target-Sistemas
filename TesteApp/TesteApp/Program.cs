using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //resposta 1 --------------------------------------------------------------------------------------------------------------------------------
            int indice = 13, soma = 0, k = 0;

            while (k < indice)
            {
                k = k + 1;
                soma = soma + k;
            }

            Console.WriteLine($"Soma = {soma}");

            //resposta 2 --------------------------------------------------------------------------------------------------------------------------------

            Console.Write("Digite um número: ");
            int numero = int.Parse(Console.ReadLine());

            if (IsFibonacci(numero))
                Console.WriteLine($"{numero} pertence a sequência de Fibonacci.");
            else
                Console.WriteLine($"{numero} não pertence a sequência de Fibonacci.");

            //resposta 3 --------------------------------------------------------------------------------------------------------------------------------

            string[] arquivoPath = Directory.GetFiles("C:\\Projects");
            string pathCompleto = "";
            foreach (string arquivo in arquivoPath)
            {
                pathCompleto = Path.GetFullPath(arquivo);
            }            
            
            try
            {
                if (File.Exists(pathCompleto))
                {

                    string jsonContent = File.ReadAllText(pathCompleto);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    List<Receita> recebidos = System.Text.Json.JsonSerializer.Deserialize<List<Receita>>(jsonContent,options);

                    List<Receita> recebidosValidos = recebidos.Where(d => d.valor > 0).ToList();

                    if (recebidosValidos.Count > 0)
                    {
                        var diaMenorReceita = recebidosValidos.OrderByDescending(d => d.valor).Last();

                        var diaMaiorReceita = recebidosValidos.OrderByDescending(d => d.valor).First();

                        double media = recebidosValidos.Average(d => d.valor);

                        List<Receita> diasAcimaMedia = recebidosValidos.Where(d => d.valor > media).ToList();

                        Console.WriteLine($"Dia com menor receita: {diaMenorReceita.valor} no dia {diaMenorReceita.dia}.");
                        Console.WriteLine($"Dia com maior receita: {diaMaiorReceita.valor} no dia {diaMaiorReceita.dia}.");
                        Console.WriteLine($"O valor médio de receita é {media}.");

                        Console.WriteLine("\nDias com receita acima da média:");
                        foreach (var receita in diasAcimaMedia)
                        {
                            Console.WriteLine($"Dia {receita.dia}: {receita.valor}");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Não foi possível ler o valor no arquivo!");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler arquivo json: {ex.Message}");
            }

            //resposta 4 --------------------------------------------------------------------------------------------------------------------------------
            try
            {
                string jsonContent = File.ReadAllText(pathCompleto);

                List<Receita> recebidos = System.Text.Json.JsonSerializer.Deserialize<List<Receita>>(jsonContent);

                List<Receita> recebidosValidos = recebidos.Where(d => d.valor > 0).ToList();

                if (recebidosValidos.Count > 0)
                {
                    double total = recebidosValidos.Sum(d => d.valor);
                    Console.WriteLine($"SP faturou 67836.43, representando {Percentual(total, 67836.43)}% do faturamento mensal");
                    Console.WriteLine($"RJ faturou 36678.66, representando {Percentual(total, 36678.66)}% do faturamento mensal");
                    Console.WriteLine($"MG faturou 29229.88, representando {Percentual(total, 29229.88)}% do faturamento mensal");
                    Console.WriteLine($"ES faturou 27165.48, representando {Percentual(total, 27165.48)}% do faturamento mensal");
                    Console.WriteLine($"Os outros somados faturaram 19849.53, representando {Percentual(total, 19849.53)}% do faturamento mensal");

                }
                else
                {
                    Console.WriteLine("Não foi possível ler o valor no arquivo!");
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler arquivo json: {ex.Message}");
            }

            //resposta 5  --------------------------------------------------------------------------------------------------------------------------------
            Console.Write("Digite a string para ser invertida: ");
            string frase = Console.ReadLine();

            string fraseInvertida = StringReversa(frase);
            Console.WriteLine($"Sting reversa: {fraseInvertida}");
            

        }

        static bool IsFibonacci(int n)
        {
            if (n < 0) return false;

            int a = 0, b = 1;
            while (a <= n)
            {
                if (a == n) return true;
                int temp = a + b;
                a = b;
                b = temp;
            }
            return false;
        }

        public struct Receita
        {
            [JsonPropertyName("dia")]
            public int dia { get; set; }
            [JsonPropertyName("valor")]
            public double valor { get; set; }
        }

        static double Percentual(double total, double valor)
        {
            double p = 0;

            p = (valor * 100) / total;

            return Math.Round(p,2);
        }

        static string StringReversa(string s)
        {
            char[] arrayInvertido = new char[s.Length];
            int j = 0;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                arrayInvertido[j] = s[i];
                j++;
            }

            return new string(arrayInvertido);
        }
    }
}