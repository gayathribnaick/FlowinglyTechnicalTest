﻿namespace ManagerTests.EmailSampleFormats
{
    public static class EmailSamples
    {

        public static string WorkingEmail = string.Format($"Hi Patricia,\r\nPlease create an expense claim for the below. Relevant details are marked up as requested…\r\n<expense><cost_centre>DEV632</cost_centre><total>35,000</total><payment_method>personal card</payment_method></expense>\r\n<note>\r\n<to>Tove</to>\r\n<from>Jani</from>\r\n<heading>Reminder</heading>\r\n<body>Don't forget me this weekend!</body>\r\n</note>\r\nFrom: William Steele\r\nSent: Friday, 16 June 2022 10:32 AM\r\nTo: Maria Washington\r\nSubject: test\r\nHi Maria,\r\nPlease create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our <description>development team’s project end celebration</description> on <date>27 April 2022</date> at 7.30pm.\r\nRegards,\r\nWilliam");
        public static string ExpenseTagMissingEmail = string.Format($"Hi Patricia,\r\nPlease create an expense claim for the below. Relevant details are marked up as requested…\r\n<expense><cost_centre>DEV632</cost_centre><total>35,000</total><payment_method>personal card</payment_method>\r\n<note>\r\n<to>Tove</to>\r\n<from>Jani</from>\r\n<heading>Reminder</heading>\r\n<body>Don't forget me this weekend!</body>\r\n</note>\r\nFrom: William Steele\r\nSent: Friday, 16 June 2022 10:32 AM\r\nTo: Maria Washington\r\nSubject: test\r\nHi Maria,\r\nPlease create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our <description>development team’s project end celebration</description> on <date>27 April 2022</date> at 7.30pm.\r\nRegards,\r\nWilliam");
    }
}
