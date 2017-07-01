<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Demo_MicroORM.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-6">  
                <h2>Lista Facturas</h2>
                    <asp:HiddenField id="hdIdFactura" runat="server"></asp:HiddenField>
                <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="false" 
                    DataKeyNames="id" OnRowCommand="gvFacturas_RowCommand" OnRowDeleting="gvFacturas_RowDeleting">

                     <Columns>  
                        <asp:BoundField DataField="nroinvoice" HeaderText="Nro. Factura" />  
                        <asp:BoundField DataField="company" HeaderText="Compañia" />  
                        <asp:BoundField DataField="customer" HeaderText="Cliente" />  
                        <asp:BoundField DataField="ammount" HeaderText="Total" />  
                         <asp:BoundField DataField="nroproducts" HeaderText="Cant. Productos" />  
                         <asp:TemplateField>
                        <ItemTemplate>
                                <asp:ImageButton ID="btnDetail" CommandName="view" CommandArgument='<%#Eval("id")%>'
                                    runat="server" ImageUrl="~/Images/details.jpg" Width="18px" Height="18px" ToolTip="Ver Detalle" />
                            </ItemTemplate>
                            <ItemStyle Width="18px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                          <asp:TemplateField>
                        <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" CommandName="delete" CommandArgument='<%#Eval("id")%>'
                                    runat="server" ImageUrl="~/Images/delete.png" Width="18px" Height="18px" ToolTip="Eliminar" />
                            </ItemTemplate>
                            <ItemStyle Width="18px" HorizontalAlign="Center" />
                        </asp:TemplateField>

                     </Columns>  

                </asp:GridView>
        </div>

        <div class="col-md-6">  
                <h2>Detalle</h2>
                               
                <asp:HiddenField id="hdIdDetalleFactura" runat="server"></asp:HiddenField>
                <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false"
                     DataKeyNames="id" OnRowCommand="gvDetalle_RowCommand" OnRowDeleting="gvDetalle_RowDeleting">
     
                    
                     <Columns>  
                        <asp:BoundField DataField="productname" HeaderText="Producto" />  
                        <asp:BoundField DataField="quantity" HeaderText="Cantidad" />  
                        <asp:BoundField DataField="unitprice" HeaderText="Precio Unitario" />  
                        <asp:BoundField DataField="subtotal" HeaderText="Sub Total" />  
                          <asp:TemplateField>
                        <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" CommandName="delete" CommandArgument='<%#Eval("id")%>'
                                    runat="server" ImageUrl="~/Images/delete.png" Width="18px" Height="18px" ToolTip="Eliminar" />
                            </ItemTemplate>
                            <ItemStyle Width="18px" HorizontalAlign="Center" />
                        </asp:TemplateField>

                     </Columns>  

                </asp:GridView>
        </div>


    </div>

    <div class="row">
        <asp:Button ID="btnNuevo" runat="server" Text="Nueva Factura" OnClick="btnNuevo_Click" />
        <div class="col-md-4">
            <h2>Invoice </h2> 
            <p>
               Nro. Factura: <asp:TextBox ID="txtNroInvoice" runat="server"></asp:TextBox>
            </p>
             <p>
               Compañia: <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
            </p>
            <p>
               Cliente: <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Detalle</h2>
             <p>
               Producto: <asp:TextBox ID="txtProducto" runat="server"></asp:TextBox>
            </p>
              <p>
               Cantidad: <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
            </p>
            <p>
               Precio Unitario: <asp:TextBox ID="txtPrecioUnitario" runat="server"></asp:TextBox>
            </p>
            <p>
               <asp:Button ID="btnAgregarDetalle" runat="server" Text="Agregar" OnClick="btnAgregarDetalle_Click" />
               <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />  
            </p>
        </div>
        <div class="col-md-4">
           <asp:GridView ID="gvDetailSession" runat="server" AutoGenerateColumns="false"
                     DataKeyNames="id" >
                        
                     <Columns>  
                        <asp:BoundField DataField="productname" HeaderText="Producto" />  
                        <asp:BoundField DataField="quantity" HeaderText="Cantidad" />  
                        <asp:BoundField DataField="unitprice" HeaderText="Precio Unitario" />  
                        <asp:BoundField DataField="subtotal" HeaderText="Sub Total" />  
                          
                        <asp:TemplateField>
                        <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" CommandName="delete" CommandArgument='<%#Eval("id")%>'
                                    runat="server" ImageUrl="~/Images/delete.png" Width="18px" Height="18px" ToolTip="Eliminar" />
                            </ItemTemplate>
                            <ItemStyle Width="18px" HorizontalAlign="Center" />
                        </asp:TemplateField>

                     </Columns>  

                </asp:GridView>
        </div>
    </div>
</asp:Content>

