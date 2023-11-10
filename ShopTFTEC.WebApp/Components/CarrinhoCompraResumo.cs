﻿using Microsoft.AspNetCore.Mvc;
using TFTEC.Web.Ecommerce.Models;
using TFTEC.Web.Ecommerce.ViewModel;

namespace TFTEC.Web.Ecommerce.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();

            //var itens = new List<CarrinhoCompraItem>()
            //{
            //    new CarrinhoCompraItem(),
            //    new CarrinhoCompraItem()
            //};

            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }
    }
}