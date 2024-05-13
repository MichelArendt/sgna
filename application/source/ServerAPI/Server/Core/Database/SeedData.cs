using ServerAPI.Server.Models.Product;
using ServerAPI.Server.Models.Users;
using ServerAPI.Server.Services;
using System.Collections.Generic;
using System.Linq;

namespace ServerAPI.Server.Database
{
    public class SeedData
    {
        public SeedData(DatabaseContext db, AppSettingsService appSettings)
        {
            // USERS
            UserModel admin = new UserModel();
            UserModel manager = new UserModel();
            UserModel waitress = new UserModel();

            admin.FullName = "Administrador";
            admin.FirstName = "Administrador";
            admin.LastName = "Test";
            admin.Email = "mail@gmail.com";
            admin.Password = "123";
            admin.Locale = "pt-BR";
            admin.StartPage = "/Home/";

            manager.FullName = "Gerente";
            manager.FirstName = "Gerente";
            manager.LastName = "Test";
            manager.Email = "mail@gmail.com";
            manager.Password = "123";
            manager.Locale = "pt-BR";
            manager.StartPage = "/Home/";

            waitress.FullName = "Garçon";
            waitress.FirstName = "Garçon";
            waitress.LastName = "Test";
            waitress.Email = "mail@gmail.com";
            waitress.Password = "123";
            waitress.Locale = "pt-BR";
            waitress.StartPage = "/Home/";

            db.Users.Add(admin);
            db.Users.Add(manager);
            db.Users.Add(waitress);

            db.SaveChanges();


            // PRODUCT
            List<ProductModel> productsToAdd =  new List<ProductModel>();

            ProductModel ROOT = new ProductModel();
            ProductModel ComboPicanha = new ProductModel();
            ProductModel BatataFrita = new ProductModel();
            ProductModel XisPicanha = new ProductModel();
            ProductModel Picanha = new ProductModel();
            ProductModel Pao = new ProductModel();
            ProductModel Salada = new ProductModel();
            ProductModel Maionese = new ProductModel();
            ProductModel Ovo = new ProductModel();
            ProductModel Oleo = new ProductModel();
            ProductModel Sal = new ProductModel();
            ProductModel Vinagre = new ProductModel();
            ProductModel TemperoVerde = new ProductModel();
            ProductModel CocaCola = new ProductModel();
            ProductModel BarcaSushi = new ProductModel();
            ProductModel Niguiri = new ProductModel();
            ProductModel Sashimi = new ProductModel();
            ProductModel Salmao = new ProductModel();
            ProductModel PeixeBranco = new ProductModel();

            ROOT.Name = "ROOT";
            ComboPicanha.Name = "Combo Picanha";
            BatataFrita.Name = "Batata Frita";
            XisPicanha.Name = "Xis Picanha";
            Picanha.Name = "Picanha";
            Salada.Name = "Salada";
            Maionese.Name = "Maionese";
            TemperoVerde.Name = "Tempero Verde";
            Oleo.Name = "Óleo";
            Ovo.Name = "Ovo";
            Vinagre.Name = "Vinagre";
            Sal.Name = "Sal";
            Pao.Name = "Pão";
            CocaCola.Name = "Coca-Cola";
            BarcaSushi.Name = "Barca de Sushi";
            Niguiri.Name = "Niguiri";
            Sashimi.Name = "Sashimi";
            Salmao.Name = "Salmão";
            PeixeBranco.Name = "Peixe Branco";

            productsToAdd.Add(ROOT);
            productsToAdd.Add(ComboPicanha);
            productsToAdd.Add(BatataFrita);
            productsToAdd.Add(XisPicanha);
            productsToAdd.Add(Picanha);
            productsToAdd.Add(Salada);
            productsToAdd.Add(Maionese);
            productsToAdd.Add(TemperoVerde);
            productsToAdd.Add(Oleo);
            productsToAdd.Add(Ovo);
            productsToAdd.Add(Vinagre);
            productsToAdd.Add(Sal);
            productsToAdd.Add(Pao);
            productsToAdd.Add(CocaCola);
            productsToAdd.Add(BarcaSushi);
            productsToAdd.Add(Niguiri);
            productsToAdd.Add(Sashimi);
            productsToAdd.Add(Salmao);
            productsToAdd.Add(PeixeBranco);

            db.Products.AddRange(productsToAdd);
            db.SaveChanges();

            // SUBPRODUCT
            List<ProductRelationshipsModel> productRelationshipsToAdd = new List<ProductRelationshipsModel>();

            ProductRelationshipsModel relationMaionese1 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationMaionese2 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationMaionese3 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationMaionese4 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationMaionese5 = new ProductRelationshipsModel();

            relationMaionese1.ProductId = db.Products.Where(p => p == Maionese).FirstOrDefault().Id;
            relationMaionese1.ParentId = db.Products.Where(p => p == XisPicanha).FirstOrDefault().Id;
            relationMaionese5.ProductId = relationMaionese4.ProductId = relationMaionese3.ProductId = relationMaionese2.ProductId = relationMaionese1.ProductId;
            relationMaionese5.ParentId = relationMaionese4.ParentId = relationMaionese3.ParentId = relationMaionese2.ParentId = relationMaionese1.ParentId;

            relationMaionese1.ChildId = db.Products.Where(p => p == TemperoVerde).FirstOrDefault().Id;
            relationMaionese2.ChildId = db.Products.Where(p => p == Oleo).FirstOrDefault().Id;
            relationMaionese3.ChildId = db.Products.Where(p => p == Ovo).FirstOrDefault().Id;
            relationMaionese4.ChildId = db.Products.Where(p => p == Vinagre).FirstOrDefault().Id;
            relationMaionese5.ChildId = db.Products.Where(p => p == Sal).FirstOrDefault().Id;

            productRelationshipsToAdd.Add(relationMaionese1);
            productRelationshipsToAdd.Add(relationMaionese2);
            productRelationshipsToAdd.Add(relationMaionese3);
            productRelationshipsToAdd.Add(relationMaionese4);
            productRelationshipsToAdd.Add(relationMaionese5);

            ProductRelationshipsModel relationPao1 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationPao2 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationPao3 = new ProductRelationshipsModel();

            relationPao1.ProductId = db.Products.Where(p => p == Pao).FirstOrDefault().Id;
            relationPao1.ParentId = db.Products.Where(p => p == XisPicanha).FirstOrDefault().Id;
            relationPao3.ProductId = relationPao2.ProductId = relationPao1.ProductId;
            relationPao3.ParentId = relationPao2.ParentId = relationPao1.ParentId;

            relationPao1.ChildId = db.Products.Where(p => p == Oleo).FirstOrDefault().Id;
            relationPao2.ChildId = db.Products.Where(p => p == Ovo).FirstOrDefault().Id;
            relationPao3.ChildId = db.Products.Where(p => p == Sal).FirstOrDefault().Id;

            productRelationshipsToAdd.Add(relationPao1);
            productRelationshipsToAdd.Add(relationPao2);
            productRelationshipsToAdd.Add(relationPao3);

            ProductRelationshipsModel relationXisPicanha1 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationXisPicanha2 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationXisPicanha3 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationXisPicanha4 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationXisPicanha5 = new ProductRelationshipsModel();

            relationXisPicanha1.ProductId = db.Products.Where(p => p == XisPicanha).FirstOrDefault().Id;
            relationXisPicanha1.ParentId = db.Products.Where(p => p == ComboPicanha).FirstOrDefault().Id;
            relationXisPicanha5.ProductId = relationXisPicanha4.ProductId = relationXisPicanha3.ProductId = relationXisPicanha2.ProductId = relationXisPicanha1.ProductId;
            relationXisPicanha5.ParentId = relationXisPicanha4.ParentId = relationXisPicanha3.ParentId = relationXisPicanha2.ParentId = relationXisPicanha1.ParentId;

            relationXisPicanha1.ChildId = db.Products.Where(p => p == Picanha).FirstOrDefault().Id;
            relationXisPicanha2.ChildId = db.Products.Where(p => p == Salada).FirstOrDefault().Id;
            relationXisPicanha3.ChildId = db.Products.Where(p => p == Maionese).FirstOrDefault().Id;
            relationXisPicanha4.ChildId = db.Products.Where(p => p == Ovo).FirstOrDefault().Id;
            relationXisPicanha5.ChildId = db.Products.Where(p => p == Pao).FirstOrDefault().Id;

            productRelationshipsToAdd.Add(relationXisPicanha1);
            productRelationshipsToAdd.Add(relationXisPicanha2);
            productRelationshipsToAdd.Add(relationXisPicanha3);
            productRelationshipsToAdd.Add(relationXisPicanha4);
            productRelationshipsToAdd.Add(relationXisPicanha5);

            ProductRelationshipsModel relationCombo1 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationCombo2 = new ProductRelationshipsModel();
            ProductRelationshipsModel relationCombo3 = new ProductRelationshipsModel();
            relationCombo1.ProductId = db.Products.Where(p => p == ComboPicanha).FirstOrDefault().Id;
            relationCombo1.ParentId = 1;
            relationCombo3.ProductId = relationCombo2.ProductId = relationCombo1.ProductId;
            relationCombo3.ParentId = relationCombo2.ParentId = relationCombo1.ParentId;

            relationCombo1.ChildId = db.Products.Where(p => p == XisPicanha).FirstOrDefault().Id;
            relationCombo2.ChildId = db.Products.Where(p => p == BatataFrita).FirstOrDefault().Id;
            relationCombo3.ChildId = db.Products.Where(p => p == CocaCola).FirstOrDefault().Id;

            productRelationshipsToAdd.Add(relationCombo1);
            productRelationshipsToAdd.Add(relationCombo2);
            productRelationshipsToAdd.Add(relationCombo3);

            ProductRelationshipsModel sushi1 = new ProductRelationshipsModel();
            ProductRelationshipsModel sushi2 = new ProductRelationshipsModel();
            ProductRelationshipsModel sushi3 = new ProductRelationshipsModel();

            sushi1.ProductId = db.Products.Where(p => p == BarcaSushi).FirstOrDefault().Id;
            sushi1.ParentId = 1;
            sushi3.ProductId = sushi2.ProductId = sushi1.ProductId;
            sushi3.ParentId = sushi2.ParentId = sushi1.ParentId;

            sushi1.ChildId = db.Products.Where(p => p == Sashimi).FirstOrDefault().Id;
            sushi2.ChildId = db.Products.Where(p => p == Niguiri).FirstOrDefault().Id;
            sushi3.ChildId = db.Products.Where(p => p == PeixeBranco).FirstOrDefault().Id;

            productRelationshipsToAdd.Add(sushi1);
            productRelationshipsToAdd.Add(sushi2);
            productRelationshipsToAdd.Add(sushi3);

            db.Products_Relationships.AddRange(productRelationshipsToAdd);
            db.SaveChanges();

            // CATEGORY
            //CategoryModel comida = new CategoryModel();
            //CategoryModel pizza = new CategoryModel();
            //CategoryModel doce = new CategoryModel();
            //CategoryModel salgada = new CategoryModel();
            //CategoryModel bebida = new CategoryModel();
            //CategoryModel refrigerante = new CategoryModel();
            //CategoryModel cerveja = new CategoryModel();

            //comida.Name = "Comida";
            //pizza.Name = "Pizza";
            //doce.Name = "Doce";
            //salgada.Name = "Salgada";
            //bebida.Name = "Bebida";
            //refrigerante.Name = "Refrigerante";
            //cerveja.Name = "Cerveja";

            //db.Categories.Add(comida);
            //db.Categories.Add(pizza);
            //db.Categories.Add(doce);
            //db.Categories.Add(salgada);
            //db.Categories.Add(bebida);
            //db.Categories.Add(refrigerante);
            //db.Categories.Add(cerveja);

            //db.SaveChanges();

            ////  SUBCATEGORY
            //SubcategoryOfModel relationComida = new SubcategoryOfModel();
            //relationComida.CategoryId = db.Categories.Where(c => c == comida).FirstOrDefault().Id;
            //relationComida.SubcategoryId = db.Categories.Where(c => c == pizza).FirstOrDefault().Id;

            //SubcategoryOfModel relationPizzaSalgada = new SubcategoryOfModel();
            //SubcategoryOfModel relationPizzaDoce = new SubcategoryOfModel();
            //relationPizzaSalgada.CategoryId = db.Categories.Where(c => c == pizza).FirstOrDefault().Id;
            //relationPizzaDoce.CategoryId = relationPizzaSalgada.CategoryId;
            //relationPizzaSalgada.SubcategoryId = db.Categories.Where(c => c == doce).FirstOrDefault().Id;
            //relationPizzaDoce.SubcategoryId = db.Categories.Where(c => c == salgada).FirstOrDefault().Id;

            //SubcategoryOfModel relationRefrigerante = new SubcategoryOfModel();
            //SubcategoryOfModel relationCerveja = new SubcategoryOfModel();
            //relationRefrigerante.CategoryId = db.Categories.Where(c => c == bebida).FirstOrDefault().Id;
            //relationCerveja.CategoryId = relationRefrigerante.CategoryId;
            //relationRefrigerante.SubcategoryId = db.Categories.Where(c => c == refrigerante).FirstOrDefault().Id;
            //relationCerveja.SubcategoryId = db.Categories.Where(c => c == cerveja).FirstOrDefault().Id;

            //db.SubcategoryOf.Add(relationComida);
            //db.SubcategoryOf.Add(relationPizzaSalgada);
            //db.SubcategoryOf.Add(relationPizzaDoce);
            //db.SubcategoryOf.Add(relationRefrigerante);
            //db.SubcategoryOf.Add(relationCerveja);

            //db.SaveChanges();
        }
    }
}
