using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DataLayer.Entities;
using DataLayer.Massive;

namespace Demo_MicroORM.Web
{
    public partial class Massive : System.Web.UI.Page
    {
        InvoiceRepository repository = new InvoiceRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarFacturas();
            }
        }

        void CargarFacturas()
        {

            List<dynamic> facturasConDetalle = new List<dynamic>();
            List<Invoice> facturas = new List<Invoice>();
            List<InvoiceDetail> detalleFacturas = new List<InvoiceDetail>();
            facturasConDetalle = repository.GetAllInvoicesAndDetails();

            facturas = repository.GetAllInvoices().Select(x => new Invoice
            {
                id = x.id,
                nroinvoice = x.nroinvoice,
                company = x.company,
                customer = x.customer,
                datecreate = x.datecreate,
            }).ToList();

            detalleFacturas = repository.GetAllInvoicesDetails().Select(x => new InvoiceDetail
            {
                id = x.id,
                idInvoice = x.idInvoice,
                productname = x.productname,
                quantity = x.quantity,
                unitprice = x.unitprice,
                subtotal = x.subtotal
            }).ToList();


            //Agregamos detalle a la Lista            
            foreach (var factura in facturas)
            {
                factura.InvoiceDetails.AddRange(detalleFacturas.Where(x => x.idInvoice == factura.id).ToList());

                factura.ammount = factura.InvoiceDetails.Select(x => x.subtotal).Sum();
                factura.nroproducts = factura.InvoiceDetails.Select(x => x.quantity).Sum();
            }

            gvFacturas.DataSource = facturas;
            gvFacturas.DataBind();

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();
        }

        void CargarDetalleFacturas(int id)
        {
            List<InvoiceDetail> detalleFacturas = new List<InvoiceDetail>();

            detalleFacturas = repository.GetAllInvoicesDetailsById(id).Select(x => new InvoiceDetail
            {
                id = x.id,
                idInvoice = x.idInvoice,
                productname = x.productname,
                quantity = x.quantity,
                unitprice = x.unitprice,
                subtotal = x.subtotal
            })
            .ToList();

            gvDetalle.DataSource = detalleFacturas;
            gvDetalle.DataBind();
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = Int32.Parse(e.CommandArgument.ToString());
            hdIdFactura.Value = id.ToString();
            if (e.CommandName == "view")
                CargarDetalleFacturas(id);

            if (e.CommandName == "delete")
                repository.Remove(id);

        }


        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idDetail = Int32.Parse(e.CommandArgument.ToString());
            int idInvoice = Int32.Parse(hdIdFactura.Value);
            hdIdDetalleFactura.Value = idDetail.ToString();

            if (e.CommandName == "delete")
                repository.RemoveDetail(idDetail);

        }

        protected void gvFacturas_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            CargarFacturas();
        }

        protected void gvDetalle_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            CargarDetalleFacturas(Convert.ToInt32(hdIdFactura.Value));
            //CargarFacturas();
        }

        void limpiar()
        {
            txtNroInvoice.Text = "";
            txtCompany.Text = "";
            txtCustomer.Text = "";

            txtProducto.Text = "";
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";

            Session["DetalleFactura"] = null;
            List<InvoiceDetail> listDetails = new List<InvoiceDetail>();
            gvDetailSession.DataSource = listDetails;
            gvDetailSession.DataBind();
            txtNroInvoice.Focus();

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            List<InvoiceDetail> listDetails = new List<InvoiceDetail>();

            if (Session["DetalleFactura"] != null)
                listDetails = (List<InvoiceDetail>)Session["DetalleFactura"];

            listDetails.Add(new InvoiceDetail
            {
                productname = txtProducto.Text,
                quantity = Convert.ToInt32(txtCantidad.Text),
                unitprice = Convert.ToDecimal(txtPrecioUnitario.Text),
                subtotal = Convert.ToInt32(txtCantidad.Text) * Convert.ToDecimal(txtPrecioUnitario.Text)
            });

            Session["DetalleFactura"] = listDetails;
            gvDetailSession.DataSource = listDetails;
            gvDetailSession.DataBind();

            txtProducto.Text = "";
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";

            txtProducto.Focus();
        }

        void LoadFacturaEdit(Invoice invoice)
        {
            txtNroInvoice.Text = invoice.nroinvoice;
            txtCompany.Text = invoice.company;
            txtCustomer.Text = invoice.customer;
        }

        void LoadFacturaDetalleEdit(InvoiceDetail detail)
        {
            txtProducto.Text = detail.productname;
            txtCantidad.Text = detail.quantity.ToString();
            txtPrecioUnitario.Text = detail.unitprice.ToString();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<InvoiceDetail> listDetails = new List<InvoiceDetail>();
            listDetails = (List<InvoiceDetail>)Session["DetalleFactura"];

            Invoice factura = new Invoice();
            factura.nroinvoice = txtNroInvoice.Text;
            factura.company = txtCompany.Text;
            factura.customer = txtCustomer.Text;
            factura.ammount = listDetails.Select(x => x.subtotal).Sum();
            factura.nroproducts = listDetails.Select(x => x.quantity).Sum();
            factura.InvoiceDetails = listDetails;

            repository.Save(factura);
            CargarFacturas();
            limpiar();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}