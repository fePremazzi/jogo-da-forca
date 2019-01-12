using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jogo_da_Forca_Final
{
    class Program
    {

        #region Métodos pertinentes à manipulação de texto

        /// <summary>
        /// Struct pertinente ao armazenamento das palavras, dicas e o 
        /// numero de dicas que tem por palavra
        /// </summary>
        struct Dados
        {
            public string palavra;
            public int numDicas;
            public string[] dicas;
        }

        /// <summary>
        /// Grava em um vetor "VetorPalavras" somente as palavras
        /// do arquivo texto "JOGO.txt"
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        static string[] GravaPalavra(string[] txt)
        {
            string[] VetorPalavras = new string[100];
            int j = 0;
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i][0] == 'P' && txt[i][1] == ':')
                {
                    VetorPalavras[j] = txt[i].Substring(2).ToUpper();
                    j++;
                }
            }
            return VetorPalavras;
        }


        /// <summary>
        /// Método que calcula a quantidade de dicas referente a cada palavra do 
        /// arquivo texto JOGO.txt e os armazena em um vetor "Num_dicas"
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        static int[] QtdeDicasPorPalavra(string[] txt)
        {
            int[] Num_dicas = new int[1000];
            int aux1 = 0, aux2 = 0, soma = 0;
            int j = 0;
            for (int i = 1; i < txt.Length; i++)
            {
                if (txt[i][0] == 'D' && txt[i][1] == ':')
                {
                    aux1++;
                    aux2++;

                }
                else if (txt[i][0] == 'P' && txt[i][1] == ':')
                {
                    Num_dicas[j] = aux1;
                    aux1 = 0;
                    j++;
                }
                if (i == txt.Length - 1)
                {
                    for (int k = 0; k < Num_dicas.Length; k++)
                    {
                        soma += Num_dicas[k];
                    }
                    Num_dicas[j] = aux2 - soma;
                }
            }
            return Num_dicas;
        }


        /// <summary>
        /// Método que grava todos as palavras e dicas dentro do struct
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="Lista"></param>
        /// <param name="VetorSoPalavras"></param>
        /// <param name="QtdeDicas"></param>
        private static void GravaString(string[] texto, Dados[] Lista, string[] VetorSoPalavras, int[] QtdeDicas)
        {
            int k = 0, k2 = 0;
            for (int i = 0; i < ContaPalavra(texto); i++)
            {
                Lista[i] = new Dados();
                Lista[i].palavra = VetorSoPalavras[i];
                Lista[i].numDicas = QtdeDicas[i];
                Lista[i].dicas = new string[Lista[i].numDicas];
                for (k = 0; k < Lista[i].numDicas; k++)
                {
                    Lista[i].dicas[k] = texto[k + k2 + 1].Substring(2);
                }
                k2 += k + 1;
            }
        }


        /// <summary>
        /// Conta quantas palavras tem no arquivo texto do JOGO.txt
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static int ContaPalavra(string[] txt)
        {
            int qtde_palavra = 0;
            for (int i = 0; i < txt.Length; i++)
                if (txt[i][0] == 'P')
                    qtde_palavra++;
            return qtde_palavra;
        }
        #endregion Métodos pertinentes à manipulação de texto Métodos pertinentes à manipulação de texto




        #region Métodos pertinentes ao funcionamento do Jogo da Forca

        /// <summary>
        /// tela Inicial do programa completa
        /// </summary>
        static void IntroduçãoInicial()
        {
            Console.WriteLine();
            Console.WriteLine(" █████████    ████     ██████     ████           █");
            Console.WriteLine("     █       █    █    █    █    █    █          █");
            Console.WriteLine("     █      █      █   █        █      █         █");
            Console.WriteLine("     █      █      █   █   ▀█   █      █     █▀▀▀█    █▀▀▀█");
            Console.WriteLine("     █       █    █    █    █    █    █             ");
            Console.WriteLine(" █████        ████     ██████     ████       █▄▄▄█    █▄▄▄█▄▄");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("      ████████     ████     ██████   ███████       █");
            Console.WriteLine("      █           █    █    █    █   █            █ █");
            Console.WriteLine("      █          █      █   ██████   █           █   █");
            Console.WriteLine("      █████      █      █   █  █     █           █▄▄▄█");
            Console.WriteLine("      █           █    █    █   █    █          █     █");
            Console.WriteLine("      █            ████     █    █   ███████   █       █");
            Console.WriteLine("\nBem vindo ao Jogo da Forca!!");
            Console.WriteLine("Responsaveis pelo desenvolvimento do jogo:");
            Console.WriteLine("Fellipe Premazzi Rego  RA:081170005");
            Console.WriteLine("Larissa Xavier de Melo RA:081170009");
            Console.WriteLine("\nItens do jogo realizados:");
            Console.WriteLine("\nJOGO               [X] >>>>> 6 pontos.");
            Console.WriteLine("DICAS  		   [X] >>>>> 2 pontos.");
            Console.WriteLine("CONTROLE DE TEMPO  [X] >>>>> 2 pontos.");
            Console.WriteLine("\nInstruções do jogo:");
            Console.WriteLine("1. Aperte apenas a letra e nao digite Enter");
            Console.WriteLine("2. Apertar F2 lhe dará uma nova dica, porém, lhe acarretará em menos uma vida");
            Console.WriteLine("3. Ao digitar uma letra repetida você será penalizado, nao seja burro");
            Console.Write("\nPressione ENTER para iniciar o jogo. . .");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Escreve uma breve introdução do jogo
        /// </summary>
        static void Introducao()
        {
            Console.WriteLine("Bem vindo ao Jogo da Forca!!");
            Console.WriteLine("Responsaveis pelo desenvolvimento do jogo:");
            Console.WriteLine("Fellipe Premazzi Rego  RA:081170005");
            Console.WriteLine("Larissa Xavier de Melo RA:081170009");
            Console.WriteLine("");
        }

        /// <summary>
        /// Coloca na tela, e no lugar certo, a letra cujo usuário escrever desde que esteja correta
        /// </summary>
        /// <param name="letra"></param>
        /// <param name="coluna"></param>
        /// <param name="linha"></param>
        static void EscreveLetra(char letra, int coluna, int linha)
        {
            Console.SetCursorPosition(coluna, linha);
            Console.Write(letra);
            LimpaTela(0, 8);


        }

        /// <summary>
        /// Printa os espaços das letras da palavra sorteada
        /// </summary>
        /// <param name="palavras"></param>
        /// <param name="num_palavra"></param>
        static void TelaInicial(Dados[] Lista, int num_palavra)
        {
            for (int i = 0; i < Lista[num_palavra].palavra.Length; i++)
            {
                if (Lista[num_palavra].palavra[i] == '-')
                {
                    placar++;
                    Console.Write("- ");
                }
                else if (Lista[num_palavra].palavra[i] == ' ')
                {
                    placar++;
                    Console.Write("  ");
                }
                else
                    Console.Write("_ ");

                if (i == Lista[num_palavra].palavra.Length - 1)
                    Console.WriteLine("");
            }
            Console.WriteLine("\nDica 1: {0}", Lista[num_palavra].dicas[0]);
        }

        /// <summary>
        /// Limpa a tela a partir de uma linha e uma coluna
        /// </summary>
        /// <param name="coluna"></param>
        /// <param name="linha"></param>
        static void LimpaTela(int coluna, int linha)
        {
            Console.SetCursorPosition(coluna, linha);
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.Write("                                      \n");
            Console.SetCursorPosition(coluna, linha);
        }

        /// <summary>
        /// Método utilizado para diminuir as "Vidas" printadas na tela
        /// </summary>
        /// <param name="coluna"></param>
        /// <param name="linha"></param>
        static void LimpaLinhas(int coluna, int linha, int qtde)
        {
            for (int i = 0; i < qtde; i++)
            {
                Console.SetCursorPosition(coluna, linha + i);
                Console.Write("                                                             \n");
            }

        }

        /// <summary>
        /// Exibe a quantidade de vidas restantes do jogador.
        /// </summary>
        /// <param name="tentativa_"></param>
        /// <param name="limite_"></param>
        static void Vidas(int tentativa_, int limite_)
        {
            //LimpaTela(45, 9);
            Console.SetCursorPosition(45, 9);
            Console.Write("Vidas restante:");
            Console.SetCursorPosition(45, 10);
            Console.Write("     ▄▄▄▄▄▄▄▄");
            Console.SetCursorPosition(45, 11);
            Console.Write("   ▄▄██    ██▄▄");
            Console.SetCursorPosition(45, 12);
            Console.Write("  ▄█ ███  ███ █▄");
            Console.SetCursorPosition(45, 13);
            Console.Write(" ▄█ ▄████████▄ █▄            ");
            Console.SetCursorPosition(45, 14);
            Console.Write("▄█▄▄██████████▄▄█▄           ");
            Console.SetCursorPosition(45, 15);
            Console.Write("███████    ███████           ");
            Console.SetCursorPosition(45, 16);
            Console.Write("██████      ██████           ");
            Console.SetCursorPosition(45, 17);
            Console.Write("███████    ███████   █   █   ");
            Console.SetCursorPosition(45, 18);
            Console.Write("██████████████████    █ █    ");
            Console.SetCursorPosition(45, 19);
            Console.Write(" ████▀▀█▀▀█▀▀████      █     ");
            Console.SetCursorPosition(45, 20);
            Console.Write("  █▄   █  █   ▄█      █ █    ");
            Console.SetCursorPosition(45, 21);
            Console.Write("   █▄▄▄▄▄▄▄▄▄▄█      █   █   ");
            int resto = limite_ - tentativa_;
            LimpaLinhas(74, 12, 10);
            switch (resto)
            {
                case 5:
                    Console.SetCursorPosition(74, 13);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 14);
                    Console.WriteLine("█");
                    Console.SetCursorPosition(74, 15);
                    Console.WriteLine("█");
                    Console.SetCursorPosition(74, 16);
                    Console.WriteLine("█");
                    Console.SetCursorPosition(74, 17);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 18);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 19);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 20);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 21);
                    Console.WriteLine("██████");
                    break;

                case 4:
                    Console.SetCursorPosition(74, 13);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 14);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 15);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 16);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 17);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 18);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 19);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 20);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 21);
                    Console.WriteLine("     █");
                    break;

                case 3:
                    Console.SetCursorPosition(74, 13);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 14);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 15);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 16);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 17);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 18);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 19);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 20);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 21);
                    Console.WriteLine("██████");
                    break;

                case 2:
                    Console.SetCursorPosition(74, 13);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 14);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 15);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 16);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 17);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 18);
                    Console.WriteLine("█");
                    Console.SetCursorPosition(74, 19);
                    Console.WriteLine("█");
                    Console.SetCursorPosition(74, 20);
                    Console.WriteLine("█");
                    Console.SetCursorPosition(74, 21);
                    Console.WriteLine("██████");
                    break;

                case 1:
                    Console.SetCursorPosition(74, 13);
                    Console.WriteLine("   ███");
                    Console.SetCursorPosition(74, 14);
                    Console.WriteLine("  █  █");
                    Console.SetCursorPosition(74, 15);
                    Console.WriteLine(" █   █");
                    Console.SetCursorPosition(74, 16);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 17);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 18);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 19);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 20);
                    Console.WriteLine("     █");
                    Console.SetCursorPosition(74, 21);
                    Console.WriteLine("     █");
                    break;

                default:
                    Console.SetCursorPosition(74, 13);
                    Console.WriteLine("██████");
                    Console.SetCursorPosition(74, 14);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 15);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 16);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 17);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 18);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 19);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 20);
                    Console.WriteLine("█    █");
                    Console.SetCursorPosition(74, 21);
                    Console.WriteLine("██████");
                    break;
            }
            Console.SetCursorPosition(0, 8);
        }

        /// <summary>
        /// Método que printa se o jogador ganhou ou perdeu
        /// </summary>
        /// <param name="placar_"></param>
        /// <param name="Lista"></param>
        /// <param name="num_palavra"></param>
        static void FimDeJogo(int placar_, Dados[] Lista, int num_palavra)
        {
            Console.Clear();
            if (placar_ == Lista[num_palavra].palavra.Length && Tempo > 0)
            {
                Congratulation();
                ganhou = true;
            }
            else
                GameOver();
        }

        /// <summary>
        /// Exibe mensagem de game over caso o jogado perca o jogo.
        /// </summary>
        static void GameOver()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("        ████████████         █         ██     ██   ██████████");
            Console.WriteLine("        █          █        █ █        █ █   █ █   █");
            Console.WriteLine("        █          █       █   █       █ █   █ █   █");
            Console.WriteLine("        █                  █   █       █  █ █  █   █");
            Console.WriteLine("        █                 █     █      █  █ █  █   █");
            Console.WriteLine("        █                 █     █      █   █   █   ███████");
            Console.WriteLine("        █      █████     █████████     █       █   █");
            Console.WriteLine("        █          █    █         █    █       █   █");
            Console.WriteLine("        █          █    █         █    █       █   █");
            Console.WriteLine("        █          █   █           █   █       █   █");
            Console.WriteLine("        ████████████   █           █   █       █   ██████████");
            Console.WriteLine("");
            Console.WriteLine("            ▄▄▄▄▄▄▄▄      █         █  █████████   ██████████");
            Console.WriteLine("          ▄▄██    ██▄▄    █         █  █           █        █");
            Console.WriteLine("         ▄█ ███  ███ █▄    █       █   █           █        █");
            Console.WriteLine("        ▄█ ▄████████▄ █▄   █       █   █           █        █");
            Console.WriteLine("       ▄█▄▄██████████▄▄█▄   █     █    █           █        █");
            Console.WriteLine("       ███████    ███████   █     █    ██████      ██████████");
            Console.WriteLine("       ██████      ██████    █   █     █           █   █");
            Console.WriteLine("       ███████    ███████    █   █     █           █    █");
            Console.WriteLine("       ██████████████████     █ █      █           █     █");
            Console.WriteLine("        ████▀█▀██▀█▀████      █ █      █           █      █");
            Console.WriteLine("         █▄  ▄▀▄▄▀▄  ▄█        █       █           █       █");
            Console.WriteLine("          █▄▄▄▄▄▄▄▄▄▄█         █       █████████   █        █");
        }

        /// <summary>
        /// Exibe os parabéns ao jogador que ganhar o jogo.
        /// </summary>
        static void Congratulation()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("        ████████████████████████");
            Console.WriteLine("        █    █            █    █                                                     ");
            Console.WriteLine("        █    █     ██     █    █");
            Console.WriteLine("        █    █    █ █     █    █");
            Console.WriteLine("        ██████      █     ██████");
            Console.WriteLine("             █      █     █ ");
            Console.WriteLine("             ██     █    ██");
            Console.WriteLine("              ██        ██            ████                  █                   █ █");
            Console.WriteLine("               ██      ██             █  █                  █                   █ █");
            Console.WriteLine("                ██    ██              ████                  █                   █ █");
            Console.WriteLine("                 ██  ██               █     ▀▀▀   █▀  ▀▀▀   █▀█  ▀▀  █▀█  █▀▀   █ █");
            Console.WriteLine("                  ████                █     █▄█▄  █   █▄█▄  █▄█  ▄▄  █ █  ▄▄█   ▄ ▄");
            Console.WriteLine("                   ██");
            Console.WriteLine("                   ██");
            Console.WriteLine("                   ██");
            Console.WriteLine("                 ██████");
            Console.WriteLine("        ████████████████████████");
            Console.WriteLine("        █ █ █ █ █ █ █ █ █ █ █ █");
            Console.WriteLine("         █ █ █ █ █ █ █ █ █ █ █ █");
            Console.WriteLine("        ████████████████████████");
        }

        /// <summary>
        /// Método para validar se o jogador quer ou nao jogar novamente
        /// </summary>
        /// <param name="play"></param>
        /// <returns></returns>
        private static bool ValidaJogarNovamente(ref char play)
        {
            bool playAgain;
            do
            {

                try
                {
                    Console.WriteLine("\nDeseja jogar novamente? S/N :");
                    play = char.Parse(Console.ReadLine().Trim().ToUpper());
                }
                catch
                {
                    Console.WriteLine("\nDigite apenas 'S' para SIM ou 'N' para NÃO!");
                }
            } while (play == ' ');


            if (play == 'S')
            {
                playAgain = true;
                Console.Clear();
            }
            else
                playAgain = false;
            return playAgain;
        }

        /// <summary>
        /// Método pertinente a subtrair 1 segundo do timer
        /// </summary>
        /// <param name="args"></param>
        static void Contador(object args)
        {
            if (Tempo > 0)
            {
                Tempo -= 1000;
                Console.SetCursorPosition(45, 5);
                Console.WriteLine("Restam: " + Tempo / 1000 + " segundos.");
                Console.SetCursorPosition(25, 11);
                if (Tempo == 0)
                {
                    Console.SetCursorPosition(45, 5);
                    Console.WriteLine("TEMPO ESGOTAAAADO!!!!");
                    Console.SetCursorPosition(25, 11);
                }
                else if (tentativa == 5)
                {
                    Console.SetCursorPosition(45, 5);
                    Console.WriteLine("  █   █                          ");
                    Console.SetCursorPosition(0, 28);
                }
                else if (ganhou)
                {
                    Console.SetCursorPosition(45, 5);
                    Console.WriteLine("                             ");
                    Console.SetCursorPosition(0, 26);
                }
            }
        }

        /// <summary>
        /// Exibe na tela as letras ja digitadas
        /// </summary>
        /// <param name="acumulado"></param>
        static void LetrasDigitadas(string acumulado)
        {
            Console.SetCursorPosition(45, 23);
            Console.Write("Teclas pressionadas: ");
            Console.SetCursorPosition(45, 24);
            Console.Write(acumulado);
            Console.SetCursorPosition(0, 8);

        }       

        #endregion  Métodos


        #region Variáveis Globais

        static int NovoTempo = 100000;
        static int Tempo = 100000;
        static int tentativa = 0;
        static bool ganhou = false;
        static int placar = 0;

        #endregion


        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 40);

            #region Manipulaçao de texto

            string[] TextoJOGOtxt = new string[1101];
            TextoJOGOtxt = File.ReadAllLines("JOGO.txt");

            Dados[] Lista = new Dados[ContaPalavra(TextoJOGOtxt)];

            string[] VetorSoPalavras = new string[ContaPalavra(TextoJOGOtxt)];

            int[] QtdeDicas = new int[1000];

            VetorSoPalavras = GravaPalavra(TextoJOGOtxt);
            QtdeDicas = QtdeDicasPorPalavra(TextoJOGOtxt);
            GravaString(TextoJOGOtxt, Lista, VetorSoPalavras, QtdeDicas);

            #endregion

            #region Jogo da Forca

            bool playAgain = false;
            char play = ' ';
            IntroduçãoInicial();
            Timer t = new Timer(Contador, null, 1000, 1000);

            do
            {
                Introducao();
                ganhou = false;
                Tempo = NovoTempo;//reset tempo

                Random rnd = new Random();
                int num_palavra_sorteada = rnd.Next(0, ContaPalavra(TextoJOGOtxt));
                string guessAcumulado = "";
                const int limite = 5;
                placar = 0;
                int neg = 0;
                tentativa = 0;
                char guess = ' ';
                int tip = 1;
                ConsoleKeyInfo tecla;




                TelaInicial(Lista, num_palavra_sorteada);
                Vidas(tentativa, limite);
                LetrasDigitadas(guessAcumulado);



                while (placar != Lista[num_palavra_sorteada].palavra.Length && tentativa < limite && Program.Tempo > 0)
                {

                    Console.Write("\nTentativas: {0} de 5 possíveis!\n", tentativa);

                    do
                    {
                        guess = ' ';
                        Console.Write("\nPalpite ou F2 para dica: ");
                        tecla = Console.ReadKey();
                        Thread.Sleep(500);

                        if (tecla.Key.ToString() == "F2")
                        {

                            if (tip < Lista[num_palavra_sorteada].numDicas)
                            {
                                LimpaLinhas(0, 7, 1);
                                Console.SetCursorPosition(0, 7);
                                LimpaLinhas(0,37, 1);
                                Console.SetCursorPosition(0, 7);
                                Console.WriteLine("Dica {0}: " + Lista[num_palavra_sorteada].dicas[tip], tip + 1);
                                tip++;
                                tentativa++;
                                LimpaLinhas(0, 9, 1);
                                Console.SetCursorPosition(0, 8);
                                Console.Write("\nTentativas: {0} de 5 possíveis!\n", tentativa);
                                LimpaLinhas(45, 9, 13);
                                Vidas(tentativa, limite);
                                Console.SetCursorPosition(25, 10);
                                guessAcumulado += "Dica - ";
                                LetrasDigitadas(guessAcumulado);

                            }
                        }
                        else if (char.IsLetter(tecla.KeyChar))
                        {
                            guess = tecla.Key.ToString().Trim().ToUpper()[0];
                            guessAcumulado += guess + " - ";
                            LetrasDigitadas(guessAcumulado);

                        }
                    } while (guess == ' ' && tip == Lista[num_palavra_sorteada].numDicas - 1);

                    LimpaTela(0, 8);
                    
                    
                    neg = 0;

                    for (int i = 0; i < Lista[num_palavra_sorteada].palavra.Length; i++)
                    {
                        if (guess == Lista[num_palavra_sorteada].palavra[i])
                        {
                            placar++;
                            EscreveLetra(guess, 2 * i, 5);
                        }
                        else
                            neg++;
                    }
                    if (neg == Lista[num_palavra_sorteada].palavra.Length)
                    {
                        if (guess != ' ')
                        {
                            tentativa++;
                            LimpaLinhas(45, 9, 13);
                        }
                    }
                    Vidas(tentativa, limite);
                }
                LimpaTela(0, 8);
                Thread.Sleep(1000);

                FimDeJogo(placar, Lista, num_palavra_sorteada);
                playAgain = ValidaJogarNovamente(ref play);
            } while (playAgain);
            #endregion
        }

    }

}
