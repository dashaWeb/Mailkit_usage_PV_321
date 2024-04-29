using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using System.Text;


internal class Program
{
    const string username = "dashakonopelko@gmail.com";
    const string password = "lmjn megr dejc cbzu";
    private static void Main(string[] args)
    {
        // Send Mail(SMTP)

        /*var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Dasha",username));
        message.To.Add(new MailboxAddress("test user", "tokiti6835@picdv.com"));

        message.Subject = "Добрий вечір ми з України! How you doin?";
        message.Importance = MessageImportance.High;

        message.Body = new TextPart()
        {
            Text = @"Hey Alice
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel convallis dui. Quisque tincidunt cursus gravida. Duis tincidunt aliquam nibh ac luctus. Maecenas eget dui at sapien pretium ultrices ac a nibh. Maecenas a diam quis mauris hendrerit ultricies. Maecenas sed turpis id ipsum eleifend iaculis eget a neque. Cras pretium odio felis, vitae cursus neque hendrerit tincidunt. Duis nisi dolor, sollicitudin ac aliquam ac, gravida a neque. Cras semper, libero scelerisque bibendum cursus, lorem nisi porta urna, ut efficitur ipsum magna eu augue. Nunc in tellus sit amet urna ultrices tempor sit amet vitae nulla. Nam venenatis ornare quam, id ullamcorper dolor placerat id. Nullam ullamcorper ex vel turpis sodales ultrices. Nunc finibus sit amet sem a volutpat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Quisque in eros a enim eleifend porttitor. In ligula risus, porta et elementum sed, pellentesque et neque.

                Fusce ante lectus, tincidunt et auctor at, aliquam ac est. Vivamus consequat dui mauris, nec porttitor tellus dapibus sit amet. Etiam vel est consectetur, faucibus nulla in, fermentum velit. Nunc vulputate quam sed enim commodo pharetra. Ut lobortis eu ligula in tempus. Etiam lobortis pretium mattis. Cras semper magna eu risus sagittis, vel tincidunt purus cursus.
            --- Bob
            "
        };

        using(var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(username, password);
            client.Send(message);
        }
*/

        // Get mails IMAP

        Console.OutputEncoding = Encoding.UTF8;
        using (var client = new ImapClient())
        {
            client.Connect("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(username, password);

            var folders = client.GetFolders(client.PersonalNamespaces[0]);
            foreach (var folder in folders)
            {
                Console.WriteLine($"{folder.Name} ({folder.FullName}) {folder.ParentFolder}");
            }


            //var fld = client.GetFolder(MailKit.SpecialFolder.Sent);
            //fld.Open(MailKit.FolderAccess.ReadOnly);

            //var messages_id = fld.Search(SearchQuery.All);
            //foreach (var item in messages_id)
            //{
            //    var message = fld.GetMessage(item);
            //    Console.WriteLine($"{item} :: {message.Date} -- {message.Subject} {message.From} {message.To}");
            //}


            //var fld = client.GetFolder(MailKit.SpecialFolder.Sent);
            //fld.Open(MailKit.FolderAccess.ReadOnly);

            //var messages_id = fld.Search(SearchQuery.All).LastOrDefault();

            //var message = fld.GetMessage(messages_id);
            //Console.WriteLine($"{messages_id} :: {message.Date} -- {message.Subject} {message.From} {message.To} \n {message.TextBody} ");


            // move to
            var fld = client.GetFolder(MailKit.SpecialFolder.Sent);
            fld.Open(MailKit.FolderAccess.ReadWrite);
            var messages_id = fld.Search(SearchQuery.All).LastOrDefault();

            //fld.MoveTo(messages_id, client.GetFolder(MailKit.SpecialFolder.Junk));
            var message = fld.GetMessage(messages_id);
            Console.WriteLine($"{messages_id} :: {message.Date} -- {message.Subject} {message.From} {message.To} \n {message.TextBody} ");

            fld.AddFlags(messages_id, MessageFlags.Deleted,true);

        }
    }
}