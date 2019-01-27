using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPDotNetWebApi01.Models
{
    public class TestUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public static IList<TestUser> GenerateDemoUsers()
        {
            List<TestUser> users = new List<TestUser>();
            users.Add(new TestUser
            {
                ID = 1,
                Name = "Test Taro",
                Email = "test.taro@sample.jp",
                Telephone = "08111222333"
            });
            users.Add(new TestUser
            {
                ID = 2,
                Name = "Test Hanako",
                Email = "test.hanako@sample.jp",
                Telephone = "08111222334"
            });
            users.Add(new TestUser
            {
                ID = 3,
                Name = "Nihon Ichiro",
                Email = "nihon.ichiro@sample.jp",
                Telephone = "08111222335"
            });
            users.Add(new TestUser
            {
                ID = 4,
                Name = "Nihon Hanako",
                Email = "nihon.hanako@sample.jp",
                Telephone = "08111222336"
            });

            return users;
        }
    }
}