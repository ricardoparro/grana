using System.Collections.Generic;
using System.Net.Mail;
using Grana.Service.Communication;
using Moq;
using Mvc.Mailer;
using NUnit.Framework;

namespace Grana.Tests.UnitTests{
  [Ignore]
    [TestFixture]
    public class MailerBaseTest : UnitTestBase<EmailService>
    {
        private MailerBase _mailerBase;
        private Mock<MailerBase> _mockMailer;
        private MailMessage _mailMessage;

        [SetUp]
        public void Setup()
        {
            MailerBase.IsTestModeEnabled = true;
            _mailerBase = new MailerBase();
            _mailMessage = new MailMessage();

            _mockMailer = new Mock<MailerBase>();
            _mockMailer.Setup(m => m.PopulateBody(_mailMessage, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()));
            InjectMock(_mockMailer);
        }

        [Test]
        public void SendEmail()
        {
            EmailService emailService = new EmailService();
            emailService.SendEmail("ricardo@raapidin.co.br", "customer@gmail.com", "Hello", "Hello John", "John");
            
            Assert.AreEqual("customer@gmail.com", _mailMessage.To);
        }


    }
}
