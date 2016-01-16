using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityFramework;
using Lohas.Business.Component.Contract;
using Lohas.Core.Ioc;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LohasContext())
            {
                
                context.Systems_SiteMap.Add(new Systems_SiteMap
                {
                    ID = Guid.NewGuid(),
                    Title = "Forms",
                    Description = "Forms",
                    ParentID = Guid.Empty
                });

                context.Systems_SiteMap.Add(new Systems_SiteMap
                {
                    ID = Guid.NewGuid(),
                    Title = "Charts",
                    Description = "Charts",
                    ParentID = Guid.Empty
                });

                context.Systems_SiteMap.Add(new Systems_SiteMap
                {
                    ID = Guid.NewGuid(),
                    Title = "Tables",
                    Description = "Tables",
                    ParentID = Guid.Empty
                });

                context.Systems_SiteMap.Add(new Systems_SiteMap
                {
                    ID = Guid.NewGuid(),
                    Title = "Bootstrap Elements",
                    Description = "Bootstrap Elements",
                    ParentID = Guid.Empty
                });

                context.Systems_SiteMap.Add(new Systems_SiteMap
                {
                    ID = Guid.NewGuid(),
                    Title = "Bootstrap Grid",
                    Description = "Bootstrap Grid",
                    ParentID = Guid.Empty
                });


                context.Systems_SiteMap.Add(new Systems_SiteMap
                {
                    ID = Guid.NewGuid(),
                    Title = "Dropdown",
                    Description = "Dropdown",
                    ParentID = Guid.Empty
                });

                context.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
