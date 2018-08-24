using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.DAL;
using Shop.Migrations;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Shop.Controllers
{

    public class AudiobooksInitializer : MigrateDatabaseToLatestVersion<AudiobooksContext, Configuration>
    {
        // Initialization of basic data
        public static void SeedAudiobooksData(AudiobooksContext context)
        {
            var categories = new List<Category>
            {
       new Category() { CategoryId=1,  CategoryName="Biografie", CategoryDescription="Opis życia postaci autentycznej, który może mieć charakter naukowy, literacki lub popularyzatorski." },
                 new Category() { CategoryId=2,  CategoryName="Ekonomia", CategoryDescription="Nauka społeczna analizująca oraz opisująca produkcję, dystrybucję oraz konsumpcję dóbr i uslug." },
                 new Category() { CategoryId=3,  CategoryName="Dla dzieci", CategoryDescription="Opowiadania i treści przeznaczone dla dzieci do dziesięciu lat." },
                 new Category() { CategoryId=4,  CategoryName="Dla młodzieży",  CategoryDescription="Opowiadania i treści przeznaczone dla młodzieży w wieku od dziesięciu do dwudziestu lat." },
                 new Category() { CategoryId=5,  CategoryName="Fantastyka", CategoryDescription="Gatunek literacki, a także filmowy, komiksowy, gier a niekiedy i malarstwa. Polega ona na kreowaniu świata przedstawionego, który w całości, lub w części różni się od rzeczywistości, na przykład przez dodanie elementów nadnaturalnych, lub stworzonej przez autora technologii." },
                 new Category() { CategoryId=6,  CategoryName="Historia", CategoryDescription="Nauka humanistyczna i społeczna, która zajmuje się badaniem przeszłości, a w znaczeniu ścisłym badaniem działań i wytworów ludzkich, aż do najstarszych poświadczonych pismem świadectw, w odróżnieniu od prehistorii, archeologii, antropologii lub historii naturalnej." },
                 new Category() { CategoryId=7,  CategoryName="Języki obce", CategoryDescription="Nauka języków obcych" },
                 new Category() { CategoryId=8,  CategoryName="Kryminał, sensacja, thiller", CategoryDescription="Odmiana powieści charakteryzująca się fabułą zorganizowaną wokół zbrodni, okoliczności dojścia do niej, dochodzenia oraz ujawnienia osoby sprawcy." },
                 new Category() { CategoryId=9,  CategoryName="Kuchnia",  CategoryDescription="Przepisy kulinarne i techniki gotowania" },
                 new Category() { CategoryId=10,  CategoryName="Lektury", CategoryDescription="Utwory literackie lub inny twór kultury objęty wykazem lektur przez Rozporządzenie Ministra Edukacji Narodowej " },
                 new Category() { CategoryId=11,  CategoryName="Literatura obyczajowa", CategoryDescription="Powieść obyczajowa to gatunek powieści, który został ukształtowany w okresie pozytywizmu. Charakterystycznym dla owej epoki było zainteresowanie ludzkim życiem, życiem społeczeństwa w jego wszelkich aspektach." },
                 new Category() { CategoryId=12,  CategoryName="Poradniki", CategoryDescription="Zbiór pomocnych rad z wielu dziedzin" },
                 new Category() { CategoryId=13,  CategoryName="Religie i wyznania",  CategoryDescription="Pozycje zawierające inforamcje z zakresu religii i wyznań na świecie" },
                 new Category() { CategoryId=14,  CategoryName="Sztuka", CategoryDescription="Dziedzina działalności ludzkiej, uprawiana przez artystów" },

            };
            categories.ForEach(k => context.Categories.AddOrUpdate(k));
            context.SaveChanges();

            var Audiobooks = new List<Audiobook>
            {
                new Audiobook() { AudiobookId=1, CategoryId=1, NameAudiobook="Audiobook 1", PriceAudiobook=18, ImageFileName="Screenshot_18.png", DateAdded=DateTime.Now, DescriptionAudiobook="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sed nunc eget lectus sollicitudin vulputate et sed magna. Mauris ut ipsum non arcu placerat fringilla aliquam pretium enim. Nunc id consectetur erat, a lobortis nisl. Etiam et vestibulum dolor. Phasellus tempor, enim at posuere placerat, quam ante iaculis ipsum, eget feugiat nibh tortor a sapien. Suspendisse congue quis augue et vehicula. Integer accumsan ipsum id tempor scelerisque. Aliquam in nibh urna. Proin ornare urna ac dui finibus, at dapibus arcu tempus. Pellentesque eget tortor quis velit bibendum dapibus eu ac eros. Nunc tristique mi massa, quis interdum felis gravida id. ",Bestseller=true, },
                new Audiobook() { AudiobookId=2, CategoryId=1, NameAudiobook="Audiobook 2", PriceAudiobook=28, ImageFileName="Screenshot_19.png", DateAdded=DateTime.Now, DescriptionAudiobook="Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki.Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60.XX w.wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker",Bestseller=false  },
                new Audiobook() { AudiobookId=3, CategoryId=4, NameAudiobook="Audiobook 3", PriceAudiobook=26, ImageFileName="Screenshot_20.png", DateAdded=DateTime.Now, DescriptionAudiobook="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sed nunc eget lectus sollicitudin vulputate et sed magna. Mauris ut ipsum non arcu placerat fringilla aliquam pretium enim. Nunc id consectetur erat, a lobortis nisl. Etiam et vestibulum dolor. Phasellus tempor, enim at posuere placerat, quam ante iaculis ipsum, eget feugiat nibh tortor a sapien. Suspendisse congue quis augue et vehicula. Integer accumsan ipsum id tempor scelerisque. Aliquam in nibh urna. Proin ornare urna ac dui finibus, at dapibus arcu tempus. Pellentesque eget tortor quis velit bibendum dapibus eu ac eros. Nunc tristique mi massa, quis interdum felis gravida id. ", Bestseller=true  },
                new Audiobook() { AudiobookId=4, CategoryId=1, NameAudiobook="Audiobook 4", PriceAudiobook=12, ImageFileName="Screenshot_21.png", DateAdded=DateTime.Now, DescriptionAudiobook="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sed nunc eget lectus sollicitudin vulputate et sed magna. Mauris ut ipsum non arcu placerat fringilla aliquam pretium enim. Nunc id consectetur erat, a lobortis nisl. Etiam et vestibulum dolor. Phasellus tempor, enim at posuere placerat, quam ante iaculis ipsum, eget feugiat nibh tortor a sapien. Suspendisse congue quis augue et vehicula. Integer accumsan ipsum id tempor scelerisque. Aliquam in nibh urna. Proin ornare urna ac dui finibus, at dapibus arcu tempus. Pellentesque eget tortor quis velit bibendum dapibus eu ac eros. Nunc tristique mi massa, quis interdum felis gravida id. ",Bestseller=true, },
                new Audiobook() { AudiobookId=5, CategoryId=1, NameAudiobook="Audiobook 5", PriceAudiobook=31, ImageFileName="Screenshot_22.png", DateAdded=DateTime.Now, DescriptionAudiobook="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sed nunc eget lectus sollicitudin vulputate et sed magna. Mauris ut ipsum non arcu placerat fringilla aliquam pretium enim. Nunc id consectetur erat, a lobortis nisl. Etiam et vestibulum dolor. Phasellus tempor, enim at posuere placerat, quam ante iaculis ipsum, eget feugiat nibh tortor a sapien. Suspendisse congue quis augue et vehicula. Integer accumsan ipsum id tempor scelerisque. Aliquam in nibh urna. Proin ornare urna ac dui finibus, at dapibus arcu tempus. Pellentesque eget tortor quis velit bibendum dapibus eu ac eros. Nunc tristique mi massa, quis interdum felis gravida id. ",Bestseller=false  },
                new Audiobook() { AudiobookId=6, CategoryId=1, NameAudiobook="Audiobook 6", PriceAudiobook =40, ImageFileName="Screenshot_23.png", DateAdded=DateTime.Now, DescriptionAudiobook="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sed nunc eget lectus sollicitudin vulputate et sed magna. Mauris ut ipsum non arcu placerat fringilla aliquam pretium enim. Nunc id consectetur erat, a lobortis nisl. Etiam et vestibulum dolor. Phasellus tempor, enim at posuere placerat, quam ante iaculis ipsum, eget feugiat nibh tortor a sapien. Suspendisse congue quis augue et vehicula. Integer accumsan ipsum id tempor scelerisque. Aliquam in nibh urna. Proin ornare urna ac dui finibus, at dapibus arcu tempus. Pellentesque eget tortor quis velit bibendum dapibus eu ac eros. Nunc tristique mi massa, quis interdum felis gravida id. ", Bestseller=true  },
            };


            Audiobooks.ForEach(z => context.Audiobooks.AddOrUpdate(z));
            context.SaveChanges();
        }

        public static void SeedUsers(AudiobooksContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "xxx@gmail.com";
            const string password = "xxx";
            const string roleName = "Admin";

            var user = userManager.FindByName(name); // Check if the user with this name already exists
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name,
                   UserData = new UserData() };
                var result = userManager.Create(user, password);
            }

            // Create an Admin role if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // Add user to Admin role if not already in role
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }

        }
    }

}