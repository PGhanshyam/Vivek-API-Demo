using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.EmailServices
{
    public static class EmailTemplates
    {
        public static string WelcomeUser(string firstName, string email, string temporaryPassword)
        {
            //return $"""
            //    <html>
            //    <body style="font-family: Arial, sans-serif;">

            //        <h2>Welcome to Superari Life</h2>

            //        <p>Hello {firstName},</p>
            //        <p>Your Superari Life account has been created successfully.</p>
            //        <p>You can use the following credentials to log in:</p>
            //        <p><strong>Email:</strong> {email}</p>
            //        <p><strong>Temporary Password:</strong> {temporaryPassword}</p>
            //        <p>For security reasons, you will be required to change your password after your first login.</p>
            //        <p>Regards,<br/>The Superari Life Team</p>
            //    </body>
            //    </html>
            //    """;

           
return $"""
    <!DOCTYPE html>
    <html>
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Welcome to Superari Life</title>
        </head>

        <body style="margin: 0; padding: 0; background-color: #F7F9FC; font-family: Arial, Helvetica, sans-serif; color: #111111;">

            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #F7F9FC; padding: 40px 15px;">

                <tr>
                    <td align="center">

                        <!-- Main Container -->
                        <table width="600" cellpadding="0" cellspacing="0" border="0" style="max-width: 600px; width: 100%; background-color: #FFFFFF; border: 1px solid #EEF0F3; border-radius: 12px; overflow: hidden;">

                            <!-- Header -->
                            <tr>
                                <td align="center" style="background-color: #0D0D0D; padding: 35px 20px;">

                                    <h1 style="margin: 0; color: #FFFFFF; font-size: 28px; font-weight: 600; letter-spacing: 0.5px;">
                                        Superari Life
                                    </h1>

                                    <p style="margin: 8px 0 0; color: #777777; font-size: 14px;">
                                        Welcome to Superari Life
                                    </p>

                                </td>
                            </tr>

                            <!-- Pink Brand Accent -->
                            <tr>
                                <td style="height: 4px; background-color: #C82468; font-size: 0; line-height: 0;">
                                    &nbsp;
                                </td>
                            </tr>

                            <!-- Content -->
                            <tr>
                                <td style="padding: 40px 35px;">

                                    <h2 style="margin: 0 0 20px; color: #111111; font-size: 24px; font-weight: 600;">
                                        Welcome, {firstName}! 👋
                                    </h2>

                                    <p style="margin: 0 0 18px; font-size: 16px; line-height: 1.6; color: #111111;">
                                        Your Superari Life account has been
                                        created successfully.
                                    </p>

                                    <p style="margin: 0 0 25px; font-size: 16px; line-height: 1.6; color: #777777;">
                                        You can use the credentials below to sign
                                        in to your account.
                                    </p>

                                    <!-- Credentials Card -->
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #F7F9FC; border: 1px solid #EEF0F3; border-radius: 8px;">

                                        <tr>
                                            <td style="padding: 20px 22px;">

                                                <p style="margin: 0 0 8px; font-size: 13px; color: #777777;">
                                                    EMAIL ADDRESS
                                                </p>

                                                <p style="margin: 0; font-size: 16px; font-weight: 600; color: #111111;">
                                                    {email}
                                                </p>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding: 0 22px 20px;">

                                                <p style="margin: 0 0 8px;font-size: 13px;color: #777777;">
                                                    TEMPORARY PASSWORD
                                                </p>

                                                <p style="margin: 0; font-size: 16px; font-weight: 600; color: #111111;">
                                                    {temporaryPassword}
                                                </p>

                                            </td>
                                        </tr>

                                    </table>

                                    <!-- Security Notice -->
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 25px; background-color: #F7F9FC; border: 1px solid #EEF0F3; border-left: 4px solid #C82468;">

                                        <tr>
                                            <td style="padding: 15px 18px;">

                                                <p style="margin: 0; font-size: 14px; line-height: 1.5; color: #111111;">
                                                    <strong>Security Notice:</strong>
                                                    For your security, you will be
                                                    required to change your password
                                                    after your first login.
                                                </p>

                                            </td>
                                        </tr>

                                    </table>

                                    <p style="margin: 30px 0 0; font-size: 15px; line-height: 1.6; color: #777777;">
                                        If you did not expect this account to be
                                        created, please contact the Superari Life
                                        support team.
                                    </p>

                                    <p style="margin: 30px 0 0; font-size: 15px; line-height: 1.6; color: #111111;">
                                        Regards,<br>
                                        <strong>The Superari Life Team</strong>
                                    </p>

                                </td>
                            </tr>

                            <!-- Divider -->
                            <tr>
                                <td style="height: 1px; background-color: #EEF0F3; font-size: 0; line-height: 0;">
                                    &nbsp;
                                </td>
                            </tr>

                            <!-- Footer -->
                            <tr>
                                <td align="center" style="background-color: #0D0D0D; padding: 22px;">

                                    <p style="margin: 0; font-size: 12px; color: #777777;">
                                        © 2026 Superari Life. All rights reserved.
                                    </p>

                                </td>
                            </tr>

                        </table>

                    </td>
                </tr>

            </table>

        </body>
    </html>
    """;

        }
    }
}
