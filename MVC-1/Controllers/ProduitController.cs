using maPremiereAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace maPremiereAppMVC.Controllers
{
    public class ProduitController : Controller
    {
        static IList<Produit> produits = new List<Produit>
        {
            new Produit(){Id=1, Nom = "Pâtes", Description = "Pâtes italiennes", Prix = 4m},
            new Produit(){Id=2, Nom = "Riz", Description = "Riz thaïlandais", Prix = 15m},
            new Produit(){Id=3, Nom = "Couscous", Description = "Couscous blanc", Prix = 10m}
        };

        public IActionResult Liste()
        {
            return View(produits);
        }

        public IActionResult Edit(int id)
        {
            //passe le id en parametre pour pouvoir savoir quoi edit plus tard
            //pour pouvoir utiliser la reference dans le form il ft deja avoir id en parametres
            var product = produits.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult EditAction(int id, Produit nouveauProduit)
        {
            var produitOriginal = produits.FirstOrDefault(produit => produit.Id == id);
            if (nouveauProduit != null)
            {
                Console.WriteLine("Nouveau Produit:");
                Console.WriteLine(nouveauProduit.Nom);
                Console.WriteLine(nouveauProduit.Description);
                Console.WriteLine(nouveauProduit.Prix);
                Console.WriteLine();
                Console.WriteLine("Produit Original");
                Console.WriteLine(produitOriginal.Nom);
                Console.WriteLine(produitOriginal.Description);
                Console.WriteLine(produitOriginal.Prix);

            // le produit original devient le nouveau
                produitOriginal.Nom = nouveauProduit.Nom;
                produitOriginal.Description = nouveauProduit.Description;
                produitOriginal.Prix = nouveauProduit.Prix;

                
            }
            
            return RedirectToAction("Liste");
        }
        public IActionResult Delete(int id) 
        {
            var produit = produits.FirstOrDefault(produit => produit.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            else
            {
                return View(produit);
            }
            
        }
        public IActionResult DeleteConfirmation(int id)
        {
            Console.WriteLine($"Received ID for deletion: {id}");
            var produit = produits.FirstOrDefault(produit => produit.Id == id);
            if (produit != null)
            {
                if (produits.Remove(produit))
                {
                    Console.WriteLine($"Product with ID {id} was removed successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to remove product with ID {id}.");
                }
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }

            return RedirectToAction("Liste");
        }
        public IActionResult Details(int id) 
        {
            // utiliser linq pour substituer sql et recuperer le produit qui match avec le parametre id
            var produit = produits.FirstOrDefault(produit => produit.Id == id);
            if (produit == null)
            {
                // si rien nest trouver
                return NotFound();
            }
            else
            {
                return View(produit);
            }
            
        }

    }
}
