namespace EventBooking.Application.Helper.ConfirmEmailMessage
{
    public static class ConfirmEmailFormat
    {
        private const string Name = "Event Booking";
        public static string GenerateHtmlContent(string message, string color, string extraContent = "")
        {
            return $@"
            <!DOCTYPE html>
            <html lang='ar'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Email confirmation</title>
                <style>
                    body {{ font-family: 'Tajawal', Arial, sans-serif; text-align: center; padding: 50px; background-color: #f4f4f4; direction: rtl; }}
                    .container {{ background: white; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); max-width: 400px; margin: auto; }}
                    h2 {{ color: {color}; }}
                    p {{ color: #333; font-size: 18px; }}
                    .logo {{ width: 100px; margin-bottom: 10px; }}
                    .button {{ text-decoration: none; color: white; background: #3498db; padding: 10px 20px; border-radius: 5px; display: inline-block; margin-top: 20px; font-size: 18px; }}
                    .button:hover {{ background: #2980b9; }}
                </style>
                <link href='https://fonts.googleapis.com/css2?family=Tajawal:wght@400;700&display=swap' rel='stylesheet'>
            </head>
            <body>
                <div class='container'>
                    <h2>{message}</h2>
                    <p>{Name}</p>
                    {extraContent}
                </div>
            </body>
            </html>";
        }
    }
}
