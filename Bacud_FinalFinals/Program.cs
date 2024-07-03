namespace Bacud_FinalFinals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // NOTE: MESSAGE.TXT MAY CONTAIN BOTH THE UNENCRYPTED MESSAGE ITSELF OR THE ENCRYPTED MESSAGE, SO DELETING IT MAY BE NECESSAARY IN SOME CASES 
            // KEY IS APPARENTLY ALREADY GIVEN BY THE PRGORAM
            // TO SEE MORE, CHECK SIR'S VIDEO

            char[,] alphabetList = new char[26, 26];

            string Message = "";
            string Key = "";

            List <char> MessageInput = new List <char> ();
            List <char> MessageOutput = new List <char> ();
            List<char> KeyChar = new List<char>();
            List<char> KeyMaskChar = new List<char>();

            for (int x = 0; x < 26; x++)
            {
                int index = 0;
                for (int y = 0; y < 26; y++)
                {
                    if (x == 0) // IF AT FIRST ROW
                    {
                        alphabetList[x, y] = (char)(65 + y);
                    }
                    else if (x > 0) // IF PAST FIRST ROW
                    {
                        if ((x + y) < 26) // INPUTS SUCCEEDING LETTERS
                        {
                            alphabetList[x, y] = alphabetList[0, x + y];
                        }
                        else // IF ALPHABET RUNS OUT, RETURNS TO START
                        {
                            alphabetList[x, y] = alphabetList[0, index];
                            index++;
                        }
                    }
                }
            }

            Console.WriteLine("==== SIMPLE MESSAGE ENCRYPTER / DECRYPTER ====\n");
            Console.WriteLine("[1] Encrypt Message\n[2] Decrypt Message\n");
            Console.Write("Input: ");

            int choiceInput = int.Parse(Console.ReadLine());

            if (choiceInput == 1) // ENCRYPTION
            {
                Console.WriteLine("\nChosen Mode: Encryption\n");

                // READING KEY FILE

                using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Bacud_Prog-DSA_Finals\Bacud_FinalFinals\Key.txt"))
                {
                    Key = sr.ReadLine().ToUpper();
                }

                for (int x = 0; x < Key.Length; x++)  // FILTERING OUT ANY SPECIAL CHARACTERS
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (Key[x] == 65 + y)
                        {
                            KeyChar.Add(Key[x]);
                            break;
                        }
                    }
                }

                Console.Write("Key Provided: ");
                foreach (char letter in KeyChar)
                {
                    Console.Write(letter);
                }
                Console.WriteLine("\n");

                Console.Write("Input your desired message: ");
                Message = Console.ReadLine().ToUpper();

                for (int i = 0; i < Message.Length; i++) // FILTERING OUT SPECIAL CHARACTERS
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (Message[i] == j + 65)
                        {
                            MessageInput.Add(Message[i]);
                            break;
                        }
                    }
                }

                // CREATION OF KEY MASK

                int Index = 0;
                for (int x = 0; x <= MessageInput.Count - 1; x++)
                {
                    if (Index > KeyChar.Count - 1)
                    {
                        Index = 0;
                    }
                    KeyMaskChar.Add(KeyChar[Index]);
                    Index++;
                }

                // ENCRYPTION

                for (int x = 0; x < MessageInput.Count; x++)
                {
                    int index1 = (((int)(MessageInput[x])) - 65);
                    int index2 = (((int)(KeyMaskChar[x])) - 65);

                    MessageOutput.Add(alphabetList[index1, index2]);
                }

                // DISPLAY

                Console.Write("\nEncrypted Message: "); 
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Bacud_Prog-DSA_Finals\Bacud_FinalFinals\ProcessedMessage.txt"))
                {
                    foreach (char letter in MessageOutput)
                    {
                        sw.Write(letter); // OUTPUTS INTO PROCESSED MESSAGE TEXT FILE
                        Console.Write(letter); // OUTPUTS INTO CONSOLE
                    }
                }

                Console.Write("\nGenerated Key Mask: ");
                foreach (char letter in KeyMaskChar)
                {
                    Console.Write(letter); 
                }
                Console.WriteLine("\n");
            }

            else if (choiceInput == 2) // DECRYPTION
            {
                Console.WriteLine("\nChosen Mode: Decryption\n");

                // READING KEY FILE

                using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Bacud_Prog-DSA_Finals\Bacud_FinalFinals\Key.txt"))
                {
                    Key = sr.ReadLine().ToUpper();
                }

                for (int x = 0; x < Key.Length; x++) // FILTERING OUT SPECIAL CHARACTERS IN KEY FILE
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (Key[x] == 65 + y)
                        {
                            KeyChar.Add(Key[x]);
                            break;
                        }
                    }
                }

                Console.Write("Key Provided: ");
                foreach (char letter in KeyChar)
                {
                    Console.Write(letter);
                }
                Console.WriteLine("\n");


                if (File.Exists(@"C:\Users\Admin\source\repos\Bacud_Prog-DSA_Finals\Bacud_FinalFinals\ProcessedMessage.txt")) // CHECKING IF ENCRYPTED MESSAGE FILE EXISTS
                {
                    Console.Write("Detected File 'ProcessedMessage.txt'!\n");
                    using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Bacud_Prog-DSA_Finals\Bacud_FinalFinals\ProcessedMessage.txt"))
                    {
                        Message = sr.ReadLine().ToUpper();
                    }
                }
                else
                {
                    Console.Write("Did Not Detect File 'ProcessedMessage.txt'!\n");
                    Console.Write("Input your encrypted message: ");
                    Message = Console.ReadLine().ToUpper();
                }

                for (int i = 0; i < Message.Length; i++) // FILTERING OUT SPECIAL CHARACTERS
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (Message[i] == j + 65)
                        {
                            MessageInput.Add(Message[i]);
                            break;
                        }
                    }
                }

                // CREATION OF KEY MASK

                int Index = 0;
                for (int x = 0; x <= MessageInput.Count - 1; x++)
                {
                    if (Index > KeyChar.Count - 1)
                    {
                        Index = 0;
                    }
                    KeyMaskChar.Add(KeyChar[Index]);
                    Index++;
                }

                // DECRYPTION

                for (int x = 0; x < KeyMaskChar.Count; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (alphabetList[(((int)(KeyMaskChar[x])) - 65), y] == Message[x])
                        {
                            MessageOutput.Add((char)(y + 65));
                            break;
                        }
                    }
                }

                // OUTPUT

                Console.Write("\nDecrypted Message: ");
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Bacud_Prog-DSA_Finals\Bacud_FinalFinals\ProcessedMessage.txt"))
                {
                    foreach (char letter in MessageOutput)
                    {
                        sw.Write(letter); // OUTPUTS PROCESSED MESSAGE INTO PROCESSED FILE
                        Console.Write(letter); // OUTPUTS INTO CONSOLE
                    }
                }

                Console.Write("\nGenerated Key Mask: ");
                foreach (char letter in KeyMaskChar)
                {
                    Console.Write(letter);
                }
                Console.WriteLine("\n");
            }
        }
    }
}