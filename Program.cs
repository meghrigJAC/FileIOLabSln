using System.IO;
using System.Text;

namespace FileIOLabSln
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../data.txt";

            //1.    Read from file
            ReadFile(filePath);

            //2.    Write to file

            filePath = "../../../WriteData.txt";
            WriteFile(filePath);          //Inefficient way to write to file using string concatenation.

            //3.   Confirm the file was created using the ReadFile method.
            Console.WriteLine("------------ Write to file line by line------------");
            ReadFile(filePath);

            //4.   Use String Builder
            filePath = "../../../WrittenDataBuilder.txt";
            Console.WriteLine("------------ Write to file once using StringBuilder ------------");
            WriteFileBuilder(filePath);     //Efficient way to write to file using the StringBuilder class.            
            ReadFile(filePath);

            Console.ReadLine();
        }
        /*
           ReadFile method: 
           Inputs: 
               - string: file name, indicates the location of the file to read.
           Output: None. 
           Functionality:
               Method uses StreamReader to read from the file one line at a time, displays the line 
               on the console and at the end display the count of the lines.
        */

        static void ReadFile(string fileName)
        {
            /*
             *  Robust Programming
                - The following conditions may cause an exception:
                - The file may not exist (IOException).
                - No authorization. 
             */

            //Step 1: Check File Exists
            if (File.Exists(fileName))
            {
                //Step 2: Declare required variables (StreamReader, string line, int count,...)
                StreamReader streamR = null;
                string line;
                int count = 0;

                //Step 3: IO is prone to exceptions. Add a try catch block.

                try
                {
                    //Step 4: Open file for reading ..initialize StreamReader
                    streamR = new StreamReader(fileName);


                    //Step 5: read from file using ReadLine (exactly like the console) 
                    //Q: When do we stop reading? What is the sentinel value? 	A: null

                    while (!streamR.EndOfStream)
                    {
                        line = streamR.ReadLine();
                        count++;
                        Console.WriteLine(line);                        //output to console
                    }
                    Console.WriteLine($"The file has {count} lines.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading file: " + e.Message);
                }
                finally
                {
                    //Step 6: close the file in the finally block.
                    if (streamR is not null)    // != null
                        streamR.Close();
                }
            }
            else
            {
                Console.WriteLine("Error: File does not exist.");
            }
        }

        /*
           WriteToFile method: 
           Inputs: 
               - string: file name, indicates the location of the file to read.
           Output: None. 
           Functionality:
               Ask the user for input and save it into a file using StreamWriter.
               The method stops when the user enter “q”. 
        */

        static void WriteFile(string fileName)
        {
            /*
             *  Robust Programming
                The following conditions may cause an exception:
                - Access to the path is denied. (WinIOError)
                - The file exists and is read-only (IOException).
                - The path name may be too long (PathTooLongException).
                - The disk may be full (IOException).
             */

            //Step 1: Declare required variables (StreamWriter, string output, ...)

            StreamWriter streamW = null;
            string output;
            int count = 0, total = 0;
            float average;


            //Step 2: IO is prone to exceptions. Best practice: add a try-catch block.
            try
            {
                //Step 3: Open file for writing: initialize StreamWriter
                streamW = new StreamWriter(fileName);

                //Step 4: Add any text content to the file using Write and WriteLine (exactly like the console)
                Console.WriteLine("Enter a message to save into a file (q to quit)");
                output = Console.ReadLine();

                while (output.ToLower() != "q")
                {
                    streamW.WriteLine(output);           //Inefficient way to write to file, one line at a time.       
                    count++;

                    Console.WriteLine("Next input to save into a file (q to quit)");
                    output = Console.ReadLine();
                }


                Console.WriteLine($"Total number of written lines is {count}");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Error! {0} Use a relative path.", e.Message);
            }
            catch (DivideByZeroException error)
            {
                Console.WriteLine("Error! {0} Cannot divide by 0.", error.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
            finally
            {
                //Step 5: Close the file in the finally block.
                if (streamW is not null) // != null
                    streamW.Close();
            }

        }
        static void WriteFileBuilder(string fileName)
        {
            /*
             *  Robust Programming
                The following conditions may cause an exception:
                - Access to the path is denied. (WinIOError)
                - The file exists and is read-only (IOException).
                - The path name may be too long (PathTooLongException).
                - The disk may be full (IOException).
             */

            //Step 1: Declare required variables (StreamWriter, string output, ...)

            StreamWriter streamW = null;
            StringBuilder builder = new();
            string output;
            int count = 0;


            try
            {
                //Step 3: Add any text content to the file using Write and WriteLine (exactly like the console)
                Console.WriteLine("Enter a message to save into a file (q to quit)");
                output = Console.ReadLine();

                while (output.ToLower() != "q")
                {
                    builder.AppendLine(output);
                    count++;

                    Console.WriteLine("Next input to save into a file (q to quit)");
                    output = Console.ReadLine();
                }

                Console.WriteLine($"Total number of written lines is {count}" );

                //Step 4: Open file for writing only when needed. Initialize StreamWriter. 
                if (count > 0)
                {
                    streamW = new StreamWriter(fileName);
                    streamW.Write(builder.ToString());                  //Write everything to the file once.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
            finally
            {
                //Step 5: Close the file in the finally block.
                if (streamW is not null) // != null
                    streamW.Close();
            }

        }
    }
}