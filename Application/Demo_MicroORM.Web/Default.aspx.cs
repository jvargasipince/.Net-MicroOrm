using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer.Dapper;
using DataLayer.Entities;
using DataLayer.Repository;

namespace Demo_MicroORM.Web
{
    public partial class _Default : Page
    {
        IInvoiceRepository repository = new InvoiceRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarFacturas();
            }
        }

        void CargarFacturas()
        {

            List<Invoice> facturas = new List<Invoice>();

            facturas = repository.GetAll();
            gvFacturas.DataSource = facturas;
            gvFacturas.DataBind();

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();
        }

        void CargarDetalleFacturas(int id)
        {
            List<InvoiceDetail> detalleFactures = repository.GetFullInvoice(id).InvoiceDetails;

            gvDetalle.DataSource = detalleFactures;
            gvDetalle.DataBind();
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int id = Int32.Parse(e.CommandArgument.ToString());
            hdIdFactura.Value = id.ToString();
            if (e.CommandName == "view")
                CargarDetalleFacturas(id);

            if (e.CommandName == "edit")
            {
                Invoice Factura = repository.GetFullInvoice(id);
                LoadFacturaEdit(Factura);
            }

            if (e.CommandName == "delete")
                repository.Remove(id);

        }


        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idDetail = Int32.Parse(e.CommandArgument.ToString());
            int idInvoice = Int32.Parse(hdIdFactura.Value);
            hdIdDetalleFactura.Value = idDetail.ToString();

            if (e.CommandName == "edit")
            {
                InvoiceDetail detalle = repository.GetFullInvoice(idInvoice).InvoiceDetails
                                        .Where(f => f.id == idDetail).SingleOrDefault();
                LoadFacturaDetalleEdit(detalle);
            }

            if (e.CommandName == "delete")
                repository.RemoveDetail(idDetail);
            
        }

        protected void gvFacturas_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            CargarFacturas();
        }


        protected void gvFacturas_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            //int idInvoice = Int32.Parse(hdIdFactura.Value);
            //Invoice Factura = repository.GetFullInvoice(idInvoice);
            //LoadFacturaEdit(Factura);
        }


        protected void gvDetalle_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            CargarDetalleFacturas(Convert.ToInt32(hdIdFactura.Value));
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
            factura.InvoiceDetails = listDetails;

            repository.Save(factura);
            CargarFacturas();
        }
    }
}