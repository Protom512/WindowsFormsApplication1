using System;
using System.Net;
using System.Text;
using System.IO;

class PNG
{
    public void Compress(string input, string output, double avg_size)
    {
        string key = "ORI0miUHvcMWnF2AAszDF0Td5pA-xjiL";
        //string input = "large-input.png";
        //string output = "tiny-output.png";

        string url = "https://api.tinypng.com/shrink";

        WebClient client = new WebClient();
        string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + key));
        client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + auth);
        string input_file, input_ext;
        input_ext = Path.GetExtension(input);
        if (input_ext == ".png")
        {
            try
            {
                FileInfo inputsize = new FileInfo(input);

                if (avg_size < inputsize.Length)
                { //ファイルサイズが大きければ
                    client.UploadData(url, File.ReadAllBytes(input));
                    /* Compression was successful, retrieve output from Location header. */
                    client.DownloadFile(client.ResponseHeaders["Location"], output);
                }
            }
            catch (WebException)
            {
                /* Something went wrong! You can parse the JSON body for details. */
                Console.WriteLine("Compression failed.");
            }
        }
    }
}