﻿using EasyMarket.Daos;
using EasyMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Supermercado
        public ActionResult SelecionarMercado()
        {
            return View(SupermercadoDao.BuscarTodos());
        }
        // GET: Produto Por Supermercado
        public ActionResult Busca(int id)
        {
            if (id != 0)
            {
                return View(ProdutoDao.BuscarPorSupermercado(id));
            }
            else
            {
                return View(ProdutoDao.BuscarTodos());
            }

        }

        // GET: Produto
        public ActionResult Index()
        {       
                return View(ProdutoDao.BuscarTodos());       
           
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View(ProdutoDao.BuscarPorId(id));
        }


        // GET: Produto/Create
        public ActionResult Create()
        {
            ViewBag.supermercados = SupermercadoDao.BuscarTodos();
            ViewBag.produtos = ProdutoDao.BuscarTodos();
            return View();
        }
        
        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Produto p = new Produto();
                p.Nome = collection["Nome"];
                p.Cod = collection["Cod"];
                p.Descricao = collection["Descricao"];
                p.PrecoCusto = Convert.ToDecimal(collection["PrecoCusto"]);
                p.Foto = collection["Foto"];
                p.Supermercado = SupermercadoDao.BuscarPorId(Convert.ToInt64(collection["Supermercado.Id"]));
                               
                if (!ProdutoDao.Persistir(p))
                {
                    return View();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.supermercados = SupermercadoDao.BuscarTodos();
            return View(ProdutoDao.BuscarPorId(id));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Produto p = new Produto();
                p.Supermercado = new Supermercado();
                p.Id = id;
                p.Nome = collection["Nome"];
                p.Cod = collection["Cod"];
                p.Descricao = collection["Descricao"];
                p.PrecoCusto = Convert.ToDecimal(collection["PrecoCusto"]);
                p.Formatado = Convert.ToString(p.PrecoCusto);
                p.Foto = collection["Foto"];               
                p.Supermercado = SupermercadoDao.BuscarPorId(Convert.ToInt32(collection["Supermercado.Id"]));
                if (p.Supermercado == null)
                {
                    Supermercado teste = new Supermercado();
                    teste.Id = 1L;
                    teste.Nome = "Makro";
                    teste.Cnpj = "00100201203217";
                    p.Supermercado = teste;
                }

                if (!ProdutoDao.Persistir(p))
                {
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ProdutoDao.BuscarPorId(id));
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ProdutoDao.Excluir(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
