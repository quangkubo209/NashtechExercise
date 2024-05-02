namespace ASPNetCoreFundamental_Day1
{
    public class WriteMessage : IWriteMessage
    {
        void IWriteMessage.WriteMessage(string message, string nameFilePath)
        {
            // Ghi log vào file
            using (StreamWriter writer = new StreamWriter(nameFilePath, true))
            {
                writer.WriteLine(message);
            }
        }

    }
}
