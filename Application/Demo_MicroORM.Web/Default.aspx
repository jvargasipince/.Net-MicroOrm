<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Demo_MicroORM.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    
    function checkSupport() {

        //Validamos si Storage es soportado
        if (typeof (Storage) !== "undefined") {
            $("#isSupport").val("True");
        } else {
            $("#isSupport").val("False");
        }
    }


    function setLocalStorage() {
        //Obtenemos el valor ingresado
        var nombre = $("#setItem").val();

        //Lo guardamos en el LocalStorage (key, value)
        sessionStorage.setItem('nombre', nombre);

        //Limpiamos Texto
        $("#setItem").val('');
    }

    function getLocalStorage() {
        //Obtenemos el valor registrado en el LocalStorage por su Key
        var nombre = localStorage.getItem('nombre');

        //Seteamos dicho valor al textBox       
        $("#getItem").val(nombre);
    }

    function filtrarNombres() {

        //Limpiamos el TextBox que recibe la lista
        $('#listaAlumnos').val('');

        //Obtenemos el valor ingresado y lo convertimos a minusculas
        var filtro = $("#filter").val().toLowerCase();

        //Obtenemos la lista de los nombres registrados en el LocalStorage         
        var listaAlumnos = JSON.parse(localStorage.getItem('listaNombres'));

        //Recorremos esa lista y validamos que contenga el texto ingresado como filtro
        listaAlumnos.forEach(function (nombre) { 
            if (nombre.toLowerCase().indexOf(filtro) > -1) {
                //Si existe, lo agregamos al TextBox filtrado
                agregarNombre(nombre);
            }                   
        });

        $("#filter").focus();

    }

    function cargarNombres() {

        //Limpiamos valores de filtro y Lista de Nombres
        $("#filter").val('');
        $('#listaAlumnos').val('');

        //Capturamos una lista existente, en caso haya sido precargada
        var listaAlumnos = JSON.parse(localStorage.getItem('listaNombres'));

        //En caso no exista, se inicializa la lista con valores por defecto
        if (listaAlumnos == null) {
            var listaNombres = ["Juan", "Pedro", "Jorge", "Miguel", "Manuel", "Alex", "Jose"];

            //Seteamos el array
            localStorage.setItem('listaNombres', JSON.stringify(listaNombres));
        }
        
        //Obtener el array registrado en el LocalStorage 
        var listaAlumnos = JSON.parse(localStorage.getItem('listaNombres'));
       
        //Recorremos esa lista y lo agregamos en nuestro TextBox
        listaAlumnos.forEach(function (nombre) {
            agregarNombre(nombre);
        });

        //Seteamos el foco
        $("#filter").focus();

    }

    function agregarNombre(nombre) {
        //Lo ponemos en el lista con salto de linea
        $('#listaAlumnos').val($('#listaAlumnos').val() + nombre + '\n');
    }

    function agregarAlumno() {
        //Obtiene nuevo nombre
        var alumno = $("#nombreAlumno").val();
        
        //Obtener el array
        var listaAlumnos = JSON.parse(localStorage.getItem('listaNombres'));

        listaAlumnos.push(alumno);

        //Seteamos el array
        localStorage.setItem('listaNombres', JSON.stringify(listaAlumnos));

        //Limpiamos el valor ingresado
        $("#nombreAlumno").val('');

        //Recargamos los nombres
        cargarNombres();
    }

</script>

    <div class="jumbotron">
        <img width="700px" height="200px" src="Images/KF_Logo.png" alt="Kaizen Force"  /> 

    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Check Support</h2>
            <p>
               ¿Is support? : <input type="text" id="isSupport" readonly style="width:75px" />
                <a class="btn btn-default" onclick="checkSupport();">Check</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Set & Get</h2>
            <p>
               Ingresar Item : <input type="text" id="setItem" style="width:200px"/>             
            </p>
             <p>
               Obtener Item : <input type="text" id="getItem" readonly style="width:200px"/>
            </p>
            <p>
                <a class="btn btn-default" onclick="setLocalStorage();">Guardar</a>
                <a class="btn btn-default" onclick="getLocalStorage();">Obtener</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Filter : </h2>
            <p>
               Ingrese filtro : <input type="text" id="filter" onkeyup="filtrarNombres();" style="width:75px"/>    
               <a class="btn btn-default" onclick="cargarNombres();">Cargar</a>         
            </p>
             <p>
             <textarea id="listaAlumnos" name="textarea" style="width:250px;height:175px;"></textarea>
            </p>

             <p>
               Nuevo Alumno : <input type="text" id="nombreAlumno" style="width:75px"/>    
               <a class="btn btn-default" onclick="agregarAlumno();">Agregar</a>         
            </p>
        </div>
    </div>

</asp:Content>

