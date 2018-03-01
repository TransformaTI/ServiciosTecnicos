Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Array
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms



Public Class frmServProgramacion1
    Inherits System.Windows.Forms.Form

    Private sqlcliente As SqlDataAdapter
    Private SQLOrden As SqlDataAdapter
    Private catalogo As DataSet
    Private CatalogoOrdenServicio As DataSet
    Private Iniciado As Boolean = False
    Public suma As Integer
    Public Imprime As Integer
    Public StatusST As String
    Public FCompromiso As DateTime
    Dim oExcelOrden As Excel.Application
    Public Sub OrdenServicioImprime(ByVal cliente As Integer, ByVal observaciones As String, ByVal Nombre As String, ByVal Pedido As Integer, ByVal FCompromiso As Date, ByVal TipoServicio As String, ByVal TipoCargo As String, ByVal DomicilioCompleto As String, ByVal EntreCalle As String, ByVal CP As Integer, ByVal CelulaDescripcion As String, ByVal CalleReferencia As String, ByVal TelCasa As String, ByVal ColoniaNombre As String, ByVal MunicipioNombre As String, ByVal Ruta As Integer, ByVal FCompromisoIniacial As DateTime)
        If Not System.IO.File.Exists(Application.StartupPath & "\" & "OrdenServicio.xls") Then
            Dim Newex As New Exception("El archivo con el formato de Orden de Servicio no se encontró.")
            Throw Newex
            Exit Sub
        End If
        Try
            oExcelOrden = New Excel.Application()
            oExcelOrden.Workbooks.Open(Application.StartupPath & "\" & "OrdenServicio.xls")
            oExcelOrden.Visible = False
            With oExcelOrden
                .Cells(9, 3) = cliente
                .Cells(9, 6) = Ruta
                .Cells(9, 8) = CelulaDescripcion
                .Cells(9, 9) = Pedido
                .Cells(11, 2) = Nombre
                .Cells(12, 2) = DomicilioCompleto
                .Cells(13, 2) = ColoniaNombre
                .Cells(14, 2) = MunicipioNombre
                .Cells(12, 7) = TelCasa
                .Cells(13, 9) = FCompromiso
                .Cells(15, 2) = EntreCalle
                .Cells(15, 4) = CalleReferencia
                .Cells(18, 3) = TipoServicio
                .Cells(18, 8) = TipoCargo
                .Cells(22, 2) = observaciones
                .Cells(14, 7) = CP
                .Cells(21, 3) = FCompromisoIniacial
            End With

            oExcelOrden.Workbooks(1).PrintOut()
            oExcelOrden.Visible = True
        Catch ex As Exception
            Throw ex
        Finally
            oExcelOrden.Cells.ClearContents()
            oExcelOrden.Workbooks(1).Close(False)
            oExcelOrden.Quit()
            oExcelOrden = Nothing
        End Try

    End Sub


    Dim oExcel As Excel.Application

    Public Sub ImprimePresupuesto(ByVal FolioPresupuesto As Integer, ByVal cliente As Integer, ByVal NombreCliente As String, ByVal CalleCompleta As String, ByVal EntreCalle As String, ByVal CP As Integer, ByVal CelulaDescripcion As String, ByVal CalleReferencia As String, ByVal TelefonoCelula As String, ByVal TelefonoCliente As String, ByVal Colonia As String, ByVal Municipio As String, ByVal Ruta As Integer, ByVal FCompromiso As Date)
        If Not System.IO.File.Exists(Application.StartupPath & "\" & "Presupuesto.xls") Then
            Dim NewEx As New Exception("El archivo con el formato de presupuesto no se encontró.")
            Throw NewEx
            Exit Sub
        End If

        Try
            oExcel = New Excel.Application()
            oExcel.Workbooks.Open(Application.StartupPath & "\" & "presupuesto.xls")
            oExcel.Visible = False
            With oExcel
                .Cells(9, 2) = FolioPresupuesto
                .Cells(11, 2) = NombreCliente
                .Cells(13, 2) = CalleCompleta
                .Cells(15, 2) = EntreCalle
                .Cells(17, 2) = CP
                .Cells(9, 4) = CelulaDescripcion
                .Cells(15, 4) = CalleReferencia
                .Cells(9, 6) = TelefonoCelula
                .Cells(11, 6) = TelefonoCliente
                .Cells(13, 6) = Colonia
                .Cells(15, 6) = Municipio
                .Cells(17, 8) = Ruta
                .Cells(11, 8) = cliente
                .Cells(17, 6) = FCompromiso
            End With

            oExcel.Workbooks(1).PrintOut()
            oExcel.Visible = True
        Catch ex As Exception
            Throw ex
        Finally
            oExcel.Cells.ClearContents()
            oExcel.Workbooks.Item(1).Close(False)
            oExcel.Quit()
            oExcel = Nothing
        End Try

    End Sub

    Private Sub PagareServicioTecnico(ByVal Pagare As Integer, ByVal Parcialidad As Integer, ByVal Dia As String, ByVal Mes As String, ByVal Año As String, ByVal Nombre As String, ByVal Direccion As String)
        If Not System.IO.File.Exists(Application.StartupPath & "\" & "PagareservicioTecnico.xls") Then
            Dim NewEx As New Exception("El archivo con el formato de presupuesto no se encontró.")
            Throw NewEx
            Exit Sub
        End If

        Try
            oExcel = New Excel.Application()
            oExcel.Workbooks.Open(Application.StartupPath & "\" & "PagareServicioTecnico.xls")
            oExcel.Visible = False
            With oExcel
                .Cells(3, 10) = Pagare
                .Cells(5, 10) = Parcialidad
                .Cells(7, 5) = Dia
                .Cells(7, 7) = Mes
                .Cells(7, 9) = Año
                .Cells(15, 2) = Nombre
                .Cells(17, 2) = Direccion
            End With
            oExcel.Workbooks(1).PrintOut()
            oExcel.Visible = True
        Catch ex As Exception
            Throw ex
        Finally
            oExcel.Cells.ClearContents()
            oExcel.Workbooks.Item(1).Close(False)
            oExcel.Quit()
            oExcel = Nothing
        End Try

    End Sub

    Private Sub SumaPuntos()
        Dim strQuery As String = "SELECT TotalPuntos FROM vwSTSumaPuntos where producto = 4 and fcompromiso = '" & dtpFecha.Text & "' and celula = " & cboCelula.Text
        Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtSumaPuntos As New DataTable("Suma")
        daSumaPuntos.Fill(dtSumaPuntos)
        If dtSumaPuntos.Rows.Count > 0 Then
            If Not IsDBNull(dtSumaPuntos.Rows(0).Item("TotalPuntos")) Then
                suma = CType(dtSumaPuntos.Rows(0).Item("TotalPuntos"), Integer)
            Else
                suma = 0
            End If
        Else
            suma = 0
        End If


    End Sub

    Private Sub LlenaServiciosTecnicos()
        Dim da As New SqlCommand("select PedidoReferencia,Cliente,FAtencion,Pedido,AñoPed,Autotanque,chofer,ayudante,ObservacionesServicioRealizado,ObservacionesServicioTecnico,StatusServicioTecnico,isnull(fcompromisoinciial,'') as FCompromisoinciial " _
                                      & "from vwSTPestanaServicioTecnico Where cliente = '" & lblContrato.Text & "' And PedidoReferencia =  '" & RTrim(lblFolio.Text) & "' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drST As SqlDataReader = da.ExecuteReader
            While drST.Read
                lblContrato.Text = CType(drST("cliente"), String)
                lblStatus.Text = CType(drST("StatusServicioTecnico"), String)
                txtTrabajoRealizado.Text = CType(drST("ObservacionesServicioRealizado"), String)
                txtObserv.Text = CType(drST("ObservacionesServicioTecnico"), String)
                lblUnidad.Text = CType(drST("autotanque"), String)
                lblTecnico.Text = CType(drST("chofer"), String)
                lblAyudante.Text = CType(drST("ayudante"), String)
                lblFAtencion.Text = CType(drST("fatencion"), String)
                lblHorario.Text = CType(drST("fcompromisoinciial"), String)
                lblAñoPed.Text = CType(drST("añoped"), String)
                lblPedidoReferencia.Text = CType(drST("PedidoReferencia"), String)
                lblFolio.Text = CType(drST("pedido"), String)

            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)

        End Try
    End Sub

    Private Sub LlenaPestañaPresupuesto()
        Dim strQuery As String = "select FolioPresupuesto,StatusPresupuesto,subtotal,ObservacionesPresupuesto,Descuento,Total from vwSTPestanaServicioTecnico where  pedido = " & lblFolio.Text & " And Cliente = " & lblContrato.Text
        Dim daPresupuesto As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtPresupuesto As New DataTable("Presupuesto")
        daPresupuesto.Fill(dtPresupuesto)
        lblNPresupuesto.Text = CType(dtPresupuesto.Rows(0).Item("FolioPresupuesto"), String)
        lblStatusPre.Text = CType(dtPresupuesto.Rows(0).Item("statuspresupuesto"), String)
        lblTotal.Text = CType(dtPresupuesto.Rows(0).Item("total"), String)
        txtObservacionesPresupuesto.Text = CType(dtPresupuesto.Rows(0).Item("ObservacionesPresupuesto"), String)
        lblSubTotal.Text = CType(dtPresupuesto.Rows(0).Item("subtotal"), String)
        lblDescuento.Text = CType(dtPresupuesto.Rows(0).Item("descuento"), String)
    End Sub


    Private Sub CargaDatosCliente()
        Try
            'Dim strConString As String = "Data Source=ERPMETRO;Initial Catalog=Galgo;User Id=sa;Password=development"
            Dim da As New SqlDataAdapter("select Cliente,Celula,Ruta,Nombre,RazonSocial,Callenombre,NumInterior,NumExterior,ColoniaNombre,cp,Status,MunicipioNombre,isnull(TelCasa,'')as TelCasa from vwSTClienteServicioTecnico Where Cliente = " & lblCliente.Text, cnnSigamet)
            Dim dt As New DataTable("Cliente")
            da.Fill(dt)
            'if apara la comparacion de que no haya dos mismos registros
            If dt.Rows.Count >= 1 Then
                lblContrato.Text = CType(dt.Rows(0).Item("Cliente"), String)
                'se pone el nombre del objeto a llenar.text = conexion con cliente(dt)
                'rows(0).item(nombre del campo de la tabla)
                lblCelula.Text = CType(dt.Rows(0).Item("Celula"), String)
                lblRuta.Text = CType(dt.Rows(0).Item("Ruta"), String)
                lblCelula.Text = CType(dt.Rows(0).Item("Celula"), String)
                lblRuta.Text = CType(dt.Rows(0).Item("Ruta"), String)
                lblNombre.Text = CType(dt.Rows(0).Item("Nombre"), String)
                lblEmpresa.Text = CType(dt.Rows(0).Item("RazonSocial"), String)
                lblCalle.Text = CType(dt.Rows(0).Item("CalleNombre"), String)
                lblNumeroInterior.Text = CType(dt.Rows(0).Item("NumInterior"), String)
                lblNumeroExterior.Text = CType(dt.Rows(0).Item("numexterior"), String)
                lblColonia.Text = CType(dt.Rows(0).Item("colonianombre"), String)
                lblCP.Text = CType(dt.Rows(0).Item("cp"), String)
                lblStatusCliente.Text = CType(dt.Rows(0).Item("status"), String)
                lblMunicipio.Text = CType(dt.Rows(0).Item("municipionombre"), String)
                lblTelefono.Text = CType(dt.Rows(0).Item("telcasa"), String)


                'para llenar el datagrid con los datos de la 
                'dgPedidos.DataSource = dt
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        ''Dim strConString As String = "Data Source=ERPMETRO;Initial Catalog=Galgo;User Id=sa;Password=development"
        'Dim da As New SqlDataAdapter("select Cliente,Celula,Ruta,Nombre,RazonSocial,Callenombre,NumInterior,NumExterior,ColoniaNombre,cp,Status,MunicipioNombre,isnull(TelCasa,'')as TelCasa from vwSTClienteServicioTecnico Where Cliente = " & lblCliente.Text, cnnSigamet)
        'Dim dt As New DataTable("Cliente")
        'da.Fill(dt)
        ''if apara la comparacion de que no haya dos mismos registros
        'If dt.Rows.Count >= 1 Then
        '    lblContrato.Text = CType(dt.Rows(0).Item("Cliente"), String)
        '    'se pone el nombre del objeto a llenar.text = conexion con cliente(dt)
        '    'rows(0).item(nombre del campo de la tabla)
        '    lblCelula.Text = CType(dt.Rows(0).Item("Celula"), String)
        '    lblRuta.Text = CType(dt.Rows(0).Item("Ruta"), String)
        '    lblCelula.Text = CType(dt.Rows(0).Item("Celula"), String)
        '    lblRuta.Text = CType(dt.Rows(0).Item("Ruta"), String)
        '    lblNombre.Text = CType(dt.Rows(0).Item("Nombre"), String)
        '    lblEmpresa.Text = CType(dt.Rows(0).Item("RazonSocial"), String)
        '    lblCalle.Text = CType(dt.Rows(0).Item("CalleNombre"), String)
        '    lblNumeroInterior.Text = CType(dt.Rows(0).Item("NumInterior"), String)
        '    lblNumeroExterior.Text = CType(dt.Rows(0).Item("numexterior"), String)
        '    lblColonia.Text = CType(dt.Rows(0).Item("colonianombre"), String)
        '    lblCP.Text = CType(dt.Rows(0).Item("cp"), String)
        '    lblStatusCliente.Text = CType(dt.Rows(0).Item("status"), String)
        '    lblMunicipio.Text = CType(dt.Rows(0).Item("municipionombre"), String)
        '    lblTelefono.Text = CType(dt.Rows(0).Item("telcasa"), String)


        '    'para llenar el datagrid con los datos de la 
        '    'dgPedidos.DataSource = dt
        'End If


    End Sub


    Private Sub OrdenAutomatica()
        Try
            Dim da As New SqlDataAdapter("select PedidoPrimero,TrabajoSolicitadoPrimero,TrabajoRealizadoPrimero,FolioPresupuestoViene,StatusPresupuestoViene,SubTotalPresupuesto,DescuentoPresupuesto,TotalPresupuesto,ObservacionesPresupuestoViene, PedidoAuto, NombreUsuario, TipoPedido, TipoServicio, TrabajoSolicitado from vwSTLlenaPedidoPresupuesto where pedidoauto = " & lblFolio.Text & "And celulaauto = " & lblCelula.Text & "And añopedauto = " & lblAñoPed.Text, cnnSigamet)
            Dim dt As New DataTable("Orden")
            da.Fill(dt)
            lblPedido.Text = CType(dt.Rows(0).Item("pedidoprimero"), String)
            txtTrabajoSolicitado.Text = CType(dt.Rows(0).Item("trabajosolicitadoprimero"), String)
            txtTrabajoRealizado.Text = CType(dt.Rows(0).Item("trabajorealizadoprimero"), String)
            lblFolioPresupuesto.Text = CType(dt.Rows(0).Item("Foliopresupuestoviene"), String)
            lblStatusPresupuesto.Text = CType(dt.Rows(0).Item("statuspresupuestoviene"), String)
            lblSubT.Text = CType(dt.Rows(0).Item("subtotalpresupuesto"), String)
            lblDesc.Text = CType(dt.Rows(0).Item("descuentopresupuesto"), String)
            lblTot.Text = CType(dt.Rows(0).Item("totalpresupuesto"), String)
            txtObservacionesPres.Text = CType(dt.Rows(0).Item("observacionespresupuestoviene"), String)
            lblAutoPedido.Text = CType(dt.Rows(0).Item("pedidoauto"), String)
            lblUsuario.Text = CType(dt.Rows(0).Item("NombreUsuario"), String)
            lblFormaPago.Text = CType(dt.Rows(0).Item("tipopedido"), String)
            lblTipoServicio.Text = CType(dt.Rows(0).Item("tiposervicio"), String)
            txtTrabSolc.Text = CType(dt.Rows(0).Item("trabajosolicitado"), String)

        Catch E As Exception
            MessageBox.Show(E.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        'Dim da As New SqlDataAdapter("select PedidoPrimero,TrabajoSolicitadoPrimero,TrabajoRealizadoPrimero,FolioPresupuestoViene,StatusPresupuestoViene,SubTotalPresupuesto,DescuentoPresupuesto,TotalPresupuesto,ObservacionesPresupuestoViene, PedidoAuto, NombreUsuario, TipoPedido, TipoServicio, TrabajoSolicitado from vwSTLlenaPedidoPresupuesto where pedidoauto = " & lblFolio.Text & "And celulaauto = " & lblCelula.Text & "And añopedauto = " & lblAñoPed.Text, cnnSigamet)
        'Dim dt As New DataTable("Orden")
        'da.Fill(dt)
        'lblPedido.Text = CType(dt.Rows(0).Item("pedidoprimero"), String)
        'txtTrabajoSolicitado.Text = CType(dt.Rows(0).Item("trabajosolicitadoprimero"), String)
        'txtTrabajoRealizado.Text = CType(dt.Rows(0).Item("trabajorealizadoprimero"), String)
        'lblFolioPresupuesto.Text = CType(dt.Rows(0).Item("Foliopresupuestoviene"), String)
        'lblStatusPresupuesto.Text = CType(dt.Rows(0).Item("statuspresupuestoviene"), String)
        'lblSubT.Text = CType(dt.Rows(0).Item("subtotalpresupuesto"), String)
        'lblDesc.Text = CType(dt.Rows(0).Item("descuentopresupuesto"), String)
        'lblTot.Text = CType(dt.Rows(0).Item("totalpresupuesto"), String)
        'txtObservacionesPres.Text = CType(dt.Rows(0).Item("observacionespresupuestoviene"), String)
        'lblAutoPedido.Text = CType(dt.Rows(0).Item("pedidoauto"), String)
        'lblUsuario.Text = CType(dt.Rows(0).Item("NombreUsuario"), String)
        'lblFormaPago.Text = CType(dt.Rows(0).Item("tipopedido"), String)
        'lblTipoServicio.Text = CType(dt.Rows(0).Item("tiposervicio"), String)
        'txtTrabSolc.Text = CType(dt.Rows(0).Item("trabajosolicitado"), String)

    End Sub



    Private Sub llenaClasificacionCliente()
        Try
            Dim da As New SqlDataAdapter("select c.cliente,c.clasificacioncliente,descripcion from cliente c join clasificacioncliente cc on c.clasificacioncliente = cc.clasificacioncliente where cliente = " & lblCliente.Text, cnnSigamet)
            Dim dt As New DataTable("Cliente")
            da.Fill(dt)
            'if apara la comparacion de que no haya dos mismos registros
            If dt.Rows.Count >= 1 Then
                lblClasificacionCliente.Text = CType(dt.Rows(0).Item("descripcion"), String)
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        ''Dim da As New SqlDataAdapter("select c.cliente,c.clasificacioncliente,descripcion from cliente c join clasificacioncliente cc on c.clasificacioncliente = cc.clasificacioncliente where cliente = " & lblCliente.Text, cnnSigamet)
        ''Dim dt As New DataTable("Cliente")
        ''da.Fill(dt)
        '''if apara la comparacion de que no haya dos mismos registros
        ''If dt.Rows.Count >= 1 Then
        ''    lblClasificacionCliente.Text = CType(dt.Rows(0).Item("descripcion"), String)
        ''End If
    End Sub

    Private Sub llena()
        'llena combo celula
        Dim Llena As New SqlDataAdapter("select celula,descripcion from celula where comercial = 1", cnnSigamet)
        Dim dtLlena As New DataTable("Celula")
        Llena.Fill(dtLlena)
        With cboCelula
            .DataSource = dtLlena
            .DisplayMember = "Descripcion"
            .ValueMember = "celula"
            .SelectedIndex = 1
        End With
    End Sub


    Private Sub ActualizaObservaciones()
        Dim strQuery As String = "select isnull(ObservacionesServicioRealizado,'')as ObservacionesServicioRealizado from SERVICIOTECNICO where  pedido = " & lblFolio.Text & " and añoped = " & lblAñoPed.Text & " And CELULA = " & lblCelula.Text
        Dim daObservaciones As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtObservaciones As New DataTable("Observaciones")
        daObservaciones.Fill(dtObservaciones)
        txtTrabajoRealizado.Text = CType(dtObservaciones.Rows(0).Item("observacionesserviciorealizado"), String)

    End Sub

    Public _Reporte As Integer


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Usuario As String, ByVal Clave As String)
        MyBase.New()
        GLOBAL_Usuario = Usuario
        GLOBAL_Password = Clave

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        oSeguridad = New SigaMetClasses.cSeguridad(Usuario, 11)

        'Add any initialization after the InitializeComponent() call
        If oSeguridad.TieneAcceso("REPROGRAMAR") Then
            btnModificar.Enabled = True

        End If
        If oSeguridad.TieneAcceso("CERRAR ORDEN") Then
            'btnCerrarOrden.Enabled = True
            Dim _FechaCorte As Date
            Dim oConfig As New SigaMetClasses.cConfig(11)
            ST_HoraCorte = CType(oConfig.Parametros("horacorteentresemana"), String)
            _FechaCorte = CType(Now.Date.ToShortDateString & " " & ST_HoraCorte, Date)
            If _FechaCorte < Now Then
                If oSeguridad.TieneAcceso("modifica fecha") Then
                    btnLiquidacion.Enabled = True
                    ' btnCerrarOrden.Enabled = True
                Else
                    btnLiquidacion.Enabled = False
                    btnAsignar.Enabled = False
                    btnCancelarOrden.Enabled = True
                    btnCancelarLiquidacion.Enabled = False
                    MenuItem1.Enabled = False
                    Catalogos.Enabled = False
                End If
            Else
                btnLiquidacion.Enabled = True
                'btnCerrarOrden.Enabled = True
                If oSeguridad.TieneAcceso("modifica fecha") Then
                    MenuItem1.Enabled = True
                    Catalogos.Enabled = True
                Else
                    MenuItem1.Enabled = False
                    MenuItem16.Enabled = False
                    Catalogos.Enabled = False
                    btnLiquidacion.Enabled = False
                    btnAsignar.Enabled = False
                    btnCancelarOrden.Enabled = True
                End If
            End If
        Else
            MenuItem1.Enabled = False
            MenuItem16.Enabled = False
            Catalogos.Enabled = False
            btnLiquidacion.Enabled = False
            btnAsignar.Enabled = False
            btnCancelarOrden.Enabled = False
        End If
        If oSeguridad.TieneAcceso("BUSCAR") Then
            'btnBuscar.Enabled = True
            MenuItem1.Enabled = True
            Catalogos.Enabled = True
        End If
        If oSeguridad.TieneAcceso("CICLOS") Then
            btnCiclos.Enabled = True
        End If
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents txtObserv As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnImprimir As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFolio As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblAyudante As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents lblFAtencion As System.Windows.Forms.Label
    Friend WithEvents tpEquipoCliente As System.Windows.Forms.TabPage
    Friend WithEvents tpServicioTecnico As System.Windows.Forms.TabPage
    Friend WithEvents tpOrdenServicio As System.Windows.Forms.TabPage
    Friend WithEvents tpCliente As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblContrato As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblNumeroInterior As System.Windows.Forms.Label
    Friend WithEvents lblNumeroExterior As System.Windows.Forms.Label
    Friend WithEvents lblCalle As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblTelefono As System.Windows.Forms.Label
    Friend WithEvents lblMunicipio As System.Windows.Forms.Label
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblColonia As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblStatusCliente As System.Windows.Forms.Label
    Friend WithEvents cboCelula As System.Windows.Forms.ComboBox
    Friend WithEvents btnReportes As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents lblStatusServicio As System.Windows.Forms.Label
    Friend WithEvents DGTSEquipoCliente As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGBCSerie As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGBCFFabricacion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGBCEquipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGBCTipoPropiedad As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGBCCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGBCCelula As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBNumEquipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTSProgramacion As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCRuta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCompromiso As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCUsuario As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoServicio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPuntos As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents lblClasificacionCliente As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblHorario As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents DGTBTecnico As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents btnCiclos As System.Windows.Forms.ToolBarButton
    Friend WithEvents grdProgramacion As System.Windows.Forms.DataGrid
    Friend WithEvents lblPuntos As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DGTBCStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblAñoPed As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents tbLlenaServicioTecnico As System.Windows.Forms.TabControl
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents tpOrdenAutomatica As System.Windows.Forms.TabPage
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents txtObservacionesPres As System.Windows.Forms.TextBox
    Friend WithEvents lblTot As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents lblSubT As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoSolicitado As System.Windows.Forms.TextBox
    Friend WithEvents txtTrabSolc As System.Windows.Forms.TextBox
    Friend WithEvents lblAutoPedido As System.Windows.Forms.Label
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents lblTipoServicio As System.Windows.Forms.Label
    Friend WithEvents lblTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents lblStatusPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblFolioPresupuesto As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblFormaPago As System.Windows.Forms.Label
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents Catalogos As System.Windows.Forms.MenuItem
    Friend WithEvents Liquidacion As System.Windows.Forms.MenuItem
    Friend WithEvents DGTBCPedidoReferencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdEquipoCliente As System.Windows.Forms.DataGrid
    Friend WithEvents lblPedidoReferencia As System.Windows.Forms.Label
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents DGTBCTipoCobro As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents btnLiquidacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelarOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnAsignar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDias As System.Windows.Forms.Label
    Friend WithEvents lblParcialidad As System.Windows.Forms.Label
    Friend WithEvents lblNumPagos As System.Windows.Forms.Label
    Friend WithEvents lblTipoPedido As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtObservacionesPresupuesto As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblNPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblStatusPre As System.Windows.Forms.Label
    Friend WithEvents lblSubTotal As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents btnCancelarLiquidacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmServProgramacion1))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnAsignar = New System.Windows.Forms.ToolBarButton()
        Me.btnLiquidacion = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarLiquidacion = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarOrden = New System.Windows.Forms.ToolBarButton()
        Me.btnImprimir = New System.Windows.Forms.ToolBarButton()
        Me.btnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.btnReportes = New System.Windows.Forms.ToolBarButton()
        Me.btnCiclos = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdProgramacion = New System.Windows.Forms.DataGrid()
        Me.DGTSProgramacion = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCPedidoReferencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCRuta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCompromiso = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCUsuario = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoServicio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPuntos = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoCobro = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBTecnico = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.tbLlenaServicioTecnico = New System.Windows.Forms.TabControl()
        Me.tpCliente = New System.Windows.Forms.TabPage()
        Me.lblClasificacionCliente = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblStatusCliente = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblTelefono = New System.Windows.Forms.Label()
        Me.lblMunicipio = New System.Windows.Forms.Label()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lblColonia = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.lblNumeroInterior = New System.Windows.Forms.Label()
        Me.lblNumeroExterior = New System.Windows.Forms.Label()
        Me.lblCalle = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.tpOrdenServicio = New System.Windows.Forms.TabPage()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtObserv = New System.Windows.Forms.TextBox()
        Me.tpServicioTecnico = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtObservacionesPresupuesto = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblNPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusPre = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.lblParcialidad = New System.Windows.Forms.Label()
        Me.lblNumPagos = New System.Windows.Forms.Label()
        Me.lblTipoPedido = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.lblPedidoReferencia = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblAñoPed = New System.Windows.Forms.Label()
        Me.lblHorario = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblContrato = New System.Windows.Forms.Label()
        Me.lblFolio = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblAyudante = New System.Windows.Forms.Label()
        Me.lblTecnico = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblUnidad = New System.Windows.Forms.Label()
        Me.lblFAtencion = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.tpEquipoCliente = New System.Windows.Forms.TabPage()
        Me.grdEquipoCliente = New System.Windows.Forms.DataGrid()
        Me.DGTSEquipoCliente = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBNumEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGBCSerie = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGBCFFabricacion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGBCEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGBCTipoPropiedad = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGBCCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGBCCelula = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tpOrdenAutomatica = New System.Windows.Forms.TabPage()
        Me.lblFormaPago = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblFolioPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusPresupuesto = New System.Windows.Forms.Label()
        Me.lblTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblTipoServicio = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.txtTrabSolc = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblAutoPedido = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtTrabajoSolicitado = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtObservacionesPres = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblTot = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblSubT = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.MenuItem14 = New System.Windows.Forms.MenuItem()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.MenuItem19 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.Catalogos = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.Liquidacion = New System.Windows.Forms.MenuItem()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCelula = New System.Windows.Forms.ComboBox()
        Me.lblStatusServicio = New System.Windows.Forms.Label()
        Me.lblPuntos = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.grdProgramacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbLlenaServicioTecnico.SuspendLayout()
        Me.tpCliente.SuspendLayout()
        Me.tpOrdenServicio.SuspendLayout()
        Me.tpServicioTecnico.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tpEquipoCliente.SuspendLayout()
        CType(Me.grdEquipoCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpOrdenAutomatica.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnModificar, Me.btnAsignar, Me.btnLiquidacion, Me.btnCancelarLiquidacion, Me.btnCancelarOrden, Me.btnImprimir, Me.btnRefrescar, Me.btnReportes, Me.btnCiclos, Me.btnCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(53, 35)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(994, 38)
        Me.ToolBar1.TabIndex = 1
        '
        'btnModificar
        '
        Me.btnModificar.Enabled = False
        Me.btnModificar.ImageIndex = 1
        Me.btnModificar.Text = "Reprogramar"
        '
        'btnAsignar
        '
        Me.btnAsignar.ImageIndex = 9
        Me.btnAsignar.Text = "Asignar"
        '
        'btnLiquidacion
        '
        Me.btnLiquidacion.ImageIndex = 6
        Me.btnLiquidacion.Text = "Liquidar"
        '
        'btnCancelarLiquidacion
        '
        Me.btnCancelarLiquidacion.ImageIndex = 11
        Me.btnCancelarLiquidacion.Text = "Canc.Liquidación"
        '
        'btnCancelarOrden
        '
        Me.btnCancelarOrden.ImageIndex = 10
        Me.btnCancelarOrden.Text = "Cancelar"
        '
        'btnImprimir
        '
        Me.btnImprimir.ImageIndex = 4
        Me.btnImprimir.Text = "Imprimir"
        '
        'btnRefrescar
        '
        Me.btnRefrescar.ImageIndex = 3
        Me.btnRefrescar.Text = "Refrescar"
        '
        'btnReportes
        '
        Me.btnReportes.ImageIndex = 8
        Me.btnReportes.Text = "Reportes"
        '
        'btnCiclos
        '
        Me.btnCiclos.Enabled = False
        Me.btnCiclos.ImageIndex = 9
        Me.btnCiclos.Text = "Ciclos"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 5
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'grdProgramacion
        '
        Me.grdProgramacion.CaptionText = "Programación"
        Me.grdProgramacion.DataMember = ""
        Me.grdProgramacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdProgramacion.Location = New System.Drawing.Point(0, 48)
        Me.grdProgramacion.Name = "grdProgramacion"
        Me.grdProgramacion.ReadOnly = True
        Me.grdProgramacion.Size = New System.Drawing.Size(1032, 320)
        Me.grdProgramacion.TabIndex = 7
        Me.grdProgramacion.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSProgramacion})
        '
        'DGTSProgramacion
        '
        Me.DGTSProgramacion.AlternatingBackColor = System.Drawing.Color.LightSteelBlue
        Me.DGTSProgramacion.DataGrid = Me.grdProgramacion
        Me.DGTSProgramacion.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCPedidoReferencia, Me.DGTBCCliente, Me.DGTBCRuta, Me.DGTBCFPedido, Me.DGTBCFCompromiso, Me.DGTBCUsuario, Me.DGTBCStatus, Me.DGTBCTipoServicio, Me.DGTBCPuntos, Me.DGTBCTipoCobro, Me.DGTBTecnico})
        Me.DGTSProgramacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSProgramacion.MappingName = "Programacion"
        '
        'DGTBCPedidoReferencia
        '
        Me.DGTBCPedidoReferencia.Format = ""
        Me.DGTBCPedidoReferencia.FormatInfo = Nothing
        Me.DGTBCPedidoReferencia.HeaderText = "Pedido"
        Me.DGTBCPedidoReferencia.MappingName = "PedidoReferencia"
        Me.DGTBCPedidoReferencia.Width = 80
        '
        'DGTBCCliente
        '
        Me.DGTBCCliente.Format = ""
        Me.DGTBCCliente.FormatInfo = Nothing
        Me.DGTBCCliente.HeaderText = "Cliente"
        Me.DGTBCCliente.MappingName = "Cliente"
        Me.DGTBCCliente.Width = 65
        '
        'DGTBCRuta
        '
        Me.DGTBCRuta.Format = ""
        Me.DGTBCRuta.FormatInfo = Nothing
        Me.DGTBCRuta.HeaderText = "Ruta"
        Me.DGTBCRuta.MappingName = "Ruta"
        Me.DGTBCRuta.Width = 40
        '
        'DGTBCFPedido
        '
        Me.DGTBCFPedido.Format = ""
        Me.DGTBCFPedido.FormatInfo = Nothing
        Me.DGTBCFPedido.HeaderText = "FPedido"
        Me.DGTBCFPedido.MappingName = "FPedido"
        Me.DGTBCFPedido.Width = 70
        '
        'DGTBCFCompromiso
        '
        Me.DGTBCFCompromiso.Format = ""
        Me.DGTBCFCompromiso.FormatInfo = Nothing
        Me.DGTBCFCompromiso.HeaderText = "FCompromiso"
        Me.DGTBCFCompromiso.MappingName = "FCompromiso"
        Me.DGTBCFCompromiso.Width = 70
        '
        'DGTBCUsuario
        '
        Me.DGTBCUsuario.Format = ""
        Me.DGTBCUsuario.FormatInfo = Nothing
        Me.DGTBCUsuario.HeaderText = "Usuario"
        Me.DGTBCUsuario.MappingName = "Usuario"
        Me.DGTBCUsuario.Width = 70
        '
        'DGTBCStatus
        '
        Me.DGTBCStatus.Format = ""
        Me.DGTBCStatus.FormatInfo = Nothing
        Me.DGTBCStatus.HeaderText = "Status Servicio Tecnico"
        Me.DGTBCStatus.MappingName = "Status"
        Me.DGTBCStatus.Width = 75
        '
        'DGTBCTipoServicio
        '
        Me.DGTBCTipoServicio.Format = ""
        Me.DGTBCTipoServicio.FormatInfo = Nothing
        Me.DGTBCTipoServicio.HeaderText = "Tipo Servicio"
        Me.DGTBCTipoServicio.MappingName = "TipoServicio"
        Me.DGTBCTipoServicio.Width = 150
        '
        'DGTBCPuntos
        '
        Me.DGTBCPuntos.Format = ""
        Me.DGTBCPuntos.FormatInfo = Nothing
        Me.DGTBCPuntos.HeaderText = "Puntos"
        Me.DGTBCPuntos.MappingName = "Puntos"
        Me.DGTBCPuntos.Width = 40
        '
        'DGTBCTipoCobro
        '
        Me.DGTBCTipoCobro.Format = ""
        Me.DGTBCTipoCobro.FormatInfo = Nothing
        Me.DGTBCTipoCobro.HeaderText = "TipoCobro"
        Me.DGTBCTipoCobro.MappingName = "TipoCobro"
        Me.DGTBCTipoCobro.Width = 80
        '
        'DGTBTecnico
        '
        Me.DGTBTecnico.Format = ""
        Me.DGTBTecnico.FormatInfo = Nothing
        Me.DGTBTecnico.HeaderText = "Técnico"
        Me.DGTBTecnico.MappingName = "Tecnico"
        Me.DGTBTecnico.Width = 200
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFecha.Location = New System.Drawing.Point(744, 8)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(96, 21)
        Me.dtpFecha.TabIndex = 8
        '
        'tbLlenaServicioTecnico
        '
        Me.tbLlenaServicioTecnico.Controls.AddRange(New System.Windows.Forms.Control() {Me.tpCliente, Me.tpOrdenServicio, Me.tpServicioTecnico, Me.tpEquipoCliente, Me.tpOrdenAutomatica})
        Me.tbLlenaServicioTecnico.Location = New System.Drawing.Point(0, 392)
        Me.tbLlenaServicioTecnico.Name = "tbLlenaServicioTecnico"
        Me.tbLlenaServicioTecnico.SelectedIndex = 0
        Me.tbLlenaServicioTecnico.Size = New System.Drawing.Size(1000, 208)
        Me.tbLlenaServicioTecnico.TabIndex = 9
        '
        'tpCliente
        '
        Me.tpCliente.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpCliente.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblClasificacionCliente, Me.Label8, Me.lblStatusCliente, Me.Label14, Me.lblTelefono, Me.lblMunicipio, Me.lblCP, Me.lblColonia, Me.Label59, Me.Label60, Me.Label61, Me.Label62, Me.lblNumeroInterior, Me.lblNumeroExterior, Me.lblCalle, Me.Label64, Me.Label65, Me.Label66, Me.lblCliente, Me.lblCelula, Me.lblRuta, Me.lblEmpresa, Me.lblNombre, Me.Label67, Me.Label68, Me.Label69, Me.Label70, Me.Label71})
        Me.tpCliente.Location = New System.Drawing.Point(4, 22)
        Me.tpCliente.Name = "tpCliente"
        Me.tpCliente.Size = New System.Drawing.Size(992, 182)
        Me.tpCliente.TabIndex = 1
        Me.tpCliente.Text = "Cliente"
        '
        'lblClasificacionCliente
        '
        Me.lblClasificacionCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblClasificacionCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClasificacionCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClasificacionCliente.Location = New System.Drawing.Point(520, 112)
        Me.lblClasificacionCliente.Name = "lblClasificacionCliente"
        Me.lblClasificacionCliente.Size = New System.Drawing.Size(384, 21)
        Me.lblClasificacionCliente.TabIndex = 246
        Me.lblClasificacionCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(400, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 14)
        Me.Label8.TabIndex = 245
        Me.Label8.Text = "Clasificación Cliente :"
        '
        'lblStatusCliente
        '
        Me.lblStatusCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblStatusCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusCliente.Location = New System.Drawing.Point(688, 80)
        Me.lblStatusCliente.Name = "lblStatusCliente"
        Me.lblStatusCliente.Size = New System.Drawing.Size(216, 21)
        Me.lblStatusCliente.TabIndex = 244
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(632, 88)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 14)
        Me.Label14.TabIndex = 243
        Me.Label14.Text = "Status:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTelefono
        '
        Me.lblTelefono.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTelefono.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTelefono.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelefono.Location = New System.Drawing.Point(520, 80)
        Me.lblTelefono.Name = "lblTelefono"
        Me.lblTelefono.Size = New System.Drawing.Size(104, 21)
        Me.lblTelefono.TabIndex = 242
        '
        'lblMunicipio
        '
        Me.lblMunicipio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMunicipio.Location = New System.Drawing.Point(688, 48)
        Me.lblMunicipio.Name = "lblMunicipio"
        Me.lblMunicipio.Size = New System.Drawing.Size(216, 21)
        Me.lblMunicipio.TabIndex = 241
        '
        'lblCP
        '
        Me.lblCP.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCP.Location = New System.Drawing.Point(520, 48)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(104, 21)
        Me.lblCP.TabIndex = 240
        '
        'lblColonia
        '
        Me.lblColonia.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColonia.Location = New System.Drawing.Point(520, 16)
        Me.lblColonia.Name = "lblColonia"
        Me.lblColonia.Size = New System.Drawing.Size(384, 21)
        Me.lblColonia.TabIndex = 239
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(632, 48)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(55, 14)
        Me.Label59.TabIndex = 238
        Me.Label59.Text = "Municipio:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(400, 88)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(52, 14)
        Me.Label60.TabIndex = 237
        Me.Label60.Text = "Telefono:"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(400, 48)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(21, 14)
        Me.Label61.TabIndex = 236
        Me.Label61.Text = "CP:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(400, 16)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(45, 14)
        Me.Label62.TabIndex = 235
        Me.Label62.Text = "Colonia:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumeroInterior
        '
        Me.lblNumeroInterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroInterior.Location = New System.Drawing.Point(272, 144)
        Me.lblNumeroInterior.Name = "lblNumeroInterior"
        Me.lblNumeroInterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroInterior.TabIndex = 234
        '
        'lblNumeroExterior
        '
        Me.lblNumeroExterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroExterior.Location = New System.Drawing.Point(80, 144)
        Me.lblNumeroExterior.Name = "lblNumeroExterior"
        Me.lblNumeroExterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroExterior.TabIndex = 233
        '
        'lblCalle
        '
        Me.lblCalle.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalle.Location = New System.Drawing.Point(80, 112)
        Me.lblCalle.Name = "lblCalle"
        Me.lblCalle.Size = New System.Drawing.Size(304, 21)
        Me.lblCalle.TabIndex = 232
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(200, 144)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(54, 14)
        Me.Label64.TabIndex = 231
        Me.Label64.Text = "#Interior:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(16, 144)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(55, 14)
        Me.Label65.TabIndex = 230
        Me.Label65.Text = "#Exterior:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(16, 112)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(32, 14)
        Me.Label66.TabIndex = 229
        Me.Label66.Text = "Calle:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.Location = New System.Drawing.Point(80, 16)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(88, 21)
        Me.lblCliente.TabIndex = 228
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.Location = New System.Drawing.Point(232, 16)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(48, 21)
        Me.lblCelula.TabIndex = 227
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(336, 16)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(48, 21)
        Me.lblRuta.TabIndex = 226
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpresa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.Location = New System.Drawing.Point(80, 80)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(304, 21)
        Me.lblEmpresa.TabIndex = 225
        '
        'lblNombre
        '
        Me.lblNombre.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(80, 48)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(304, 21)
        Me.lblNombre.TabIndex = 224
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(16, 80)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(51, 14)
        Me.Label67.TabIndex = 223
        Me.Label67.Text = "Empresa:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(16, 48)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(48, 14)
        Me.Label68.TabIndex = 222
        Me.Label68.Text = "Nombre:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(184, 16)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(38, 14)
        Me.Label69.TabIndex = 221
        Me.Label69.Text = "Celula:"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(288, 16)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(31, 14)
        Me.Label70.TabIndex = 220
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(16, 16)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(51, 14)
        Me.Label71.TabIndex = 219
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tpOrdenServicio
        '
        Me.tpOrdenServicio.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpOrdenServicio.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label24, Me.txtTrabajoRealizado, Me.Label23, Me.txtObserv})
        Me.tpOrdenServicio.Location = New System.Drawing.Point(4, 22)
        Me.tpOrdenServicio.Name = "tpOrdenServicio"
        Me.tpOrdenServicio.Size = New System.Drawing.Size(992, 182)
        Me.tpOrdenServicio.TabIndex = 3
        Me.tpOrdenServicio.Text = "Observaciones"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(496, 8)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(93, 14)
        Me.Label24.TabIndex = 222
        Me.Label24.Text = "Trabajo Realizado"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(488, 32)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ReadOnly = True
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(480, 144)
        Me.txtTrabajoRealizado.TabIndex = 221
        Me.txtTrabajoRealizado.Text = ""
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(8, 8)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 14)
        Me.Label23.TabIndex = 220
        Me.Label23.Text = "Trabajo Solicitado"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtObserv
        '
        Me.txtObserv.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtObserv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObserv.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObserv.Location = New System.Drawing.Point(0, 32)
        Me.txtObserv.Multiline = True
        Me.txtObserv.Name = "txtObserv"
        Me.txtObserv.ReadOnly = True
        Me.txtObserv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObserv.Size = New System.Drawing.Size(440, 144)
        Me.txtObserv.TabIndex = 203
        Me.txtObserv.Text = ""
        '
        'tpServicioTecnico
        '
        Me.tpServicioTecnico.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpServicioTecnico.Controls.AddRange(New System.Windows.Forms.Control() {Me.GroupBox2, Me.GroupBox1, Me.lblPedidoReferencia, Me.Label20, Me.lblAñoPed, Me.lblHorario, Me.Label18, Me.Label4, Me.lblContrato, Me.lblFolio, Me.lblStatus, Me.Label15, Me.Label16, Me.Label12, Me.Label6, Me.Label11, Me.Label30, Me.lblAyudante, Me.lblTecnico, Me.Label42, Me.Label39, Me.lblUnidad, Me.lblFAtencion, Me.Label41, Me.Label40, Me.Label28, Me.Label26})
        Me.tpServicioTecnico.Location = New System.Drawing.Point(4, 22)
        Me.tpServicioTecnico.Name = "tpServicioTecnico"
        Me.tpServicioTecnico.Size = New System.Drawing.Size(992, 182)
        Me.tpServicioTecnico.TabIndex = 2
        Me.tpServicioTecnico.Text = "Servicio Técnico"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtObservacionesPresupuesto, Me.Label17, Me.Label7, Me.Label10, Me.lblTotal, Me.lblDescuento, Me.Label3, Me.Label5, Me.lblNPresupuesto, Me.lblStatusPre, Me.lblSubTotal, Me.Label13})
        Me.GroupBox2.Location = New System.Drawing.Point(624, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 168)
        Me.GroupBox2.TabIndex = 334
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Presupuesto"
        '
        'txtObservacionesPresupuesto
        '
        Me.txtObservacionesPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtObservacionesPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesPresupuesto.Location = New System.Drawing.Point(168, 40)
        Me.txtObservacionesPresupuesto.Multiline = True
        Me.txtObservacionesPresupuesto.Name = "txtObservacionesPresupuesto"
        Me.txtObservacionesPresupuesto.ReadOnly = True
        Me.txtObservacionesPresupuesto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesPresupuesto.Size = New System.Drawing.Size(184, 120)
        Me.txtObservacionesPresupuesto.TabIndex = 334
        Me.txtObservacionesPresupuesto.Text = ""
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(176, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(145, 14)
        Me.Label17.TabIndex = 333
        Me.Label17.Text = "Observaciones Presupuesto:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 14)
        Me.Label7.TabIndex = 332
        Me.Label7.Text = "Descuento:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 139)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 14)
        Me.Label10.TabIndex = 331
        Me.Label10.Text = "Total:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(72, 136)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(80, 21)
        Me.lblTotal.TabIndex = 330
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDescuento
        '
        Me.lblDescuento.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblDescuento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescuento.Location = New System.Drawing.Point(72, 112)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(80, 21)
        Me.lblDescuento.TabIndex = 329
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 14)
        Me.Label3.TabIndex = 328
        Me.Label3.Text = "Status:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 327
        Me.Label5.Text = " Folio:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNPresupuesto
        '
        Me.lblNPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPresupuesto.Location = New System.Drawing.Point(72, 40)
        Me.lblNPresupuesto.Name = "lblNPresupuesto"
        Me.lblNPresupuesto.Size = New System.Drawing.Size(80, 21)
        Me.lblNPresupuesto.TabIndex = 326
        Me.lblNPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusPre
        '
        Me.lblStatusPre.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblStatusPre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusPre.Location = New System.Drawing.Point(72, 64)
        Me.lblStatusPre.Name = "lblStatusPre"
        Me.lblStatusPre.Size = New System.Drawing.Size(80, 21)
        Me.lblStatusPre.TabIndex = 325
        Me.lblStatusPre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSubTotal
        '
        Me.lblSubTotal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTotal.Location = New System.Drawing.Point(72, 88)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.Size = New System.Drawing.Size(80, 21)
        Me.lblSubTotal.TabIndex = 324
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 91)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 14)
        Me.Label13.TabIndex = 323
        Me.Label13.Text = "SubTotal:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblDias, Me.lblParcialidad, Me.lblNumPagos, Me.lblTipoPedido, Me.Label38, Me.Label31, Me.Label27, Me.Label25, Me.Label43})
        Me.GroupBox1.Location = New System.Drawing.Point(400, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 168)
        Me.GroupBox1.TabIndex = 333
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pagaré"
        '
        'lblDias
        '
        Me.lblDias.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblDias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDias.Location = New System.Drawing.Point(84, 122)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(68, 21)
        Me.lblDias.TabIndex = 345
        Me.lblDias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblParcialidad
        '
        Me.lblParcialidad.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblParcialidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblParcialidad.Location = New System.Drawing.Point(84, 90)
        Me.lblParcialidad.Name = "lblParcialidad"
        Me.lblParcialidad.Size = New System.Drawing.Size(124, 21)
        Me.lblParcialidad.TabIndex = 344
        Me.lblParcialidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumPagos
        '
        Me.lblNumPagos.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumPagos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumPagos.Location = New System.Drawing.Point(84, 58)
        Me.lblNumPagos.Name = "lblNumPagos"
        Me.lblNumPagos.Size = New System.Drawing.Size(124, 21)
        Me.lblNumPagos.TabIndex = 343
        Me.lblNumPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTipoPedido
        '
        Me.lblTipoPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTipoPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTipoPedido.Location = New System.Drawing.Point(84, 26)
        Me.lblTipoPedido.Name = "lblTipoPedido"
        Me.lblTipoPedido.Size = New System.Drawing.Size(124, 21)
        Me.lblTipoPedido.TabIndex = 342
        Me.lblTipoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(4, 125)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(37, 14)
        Me.Label38.TabIndex = 341
        Me.Label38.Text = " Cada:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(4, 93)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(76, 14)
        Me.Label31.TabIndex = 340
        Me.Label31.Text = " Parcialidades:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(4, 61)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(65, 14)
        Me.Label27.TabIndex = 339
        Me.Label27.Text = " NúmPagos:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(4, 29)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(67, 14)
        Me.Label25.TabIndex = 338
        Me.Label25.Text = " TipoPedido:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(168, 125)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(29, 14)
        Me.Label43.TabIndex = 332
        Me.Label43.Text = " Días"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPedidoReferencia
        '
        Me.lblPedidoReferencia.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblPedidoReferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedidoReferencia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedidoReferencia.Location = New System.Drawing.Point(80, 72)
        Me.lblPedidoReferencia.Name = "lblPedidoReferencia"
        Me.lblPedidoReferencia.Size = New System.Drawing.Size(80, 21)
        Me.lblPedidoReferencia.TabIndex = 327
        Me.lblPedidoReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(16, 43)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(46, 14)
        Me.Label20.TabIndex = 326
        Me.Label20.Text = "AñoPed:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAñoPed
        '
        Me.lblAñoPed.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAñoPed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAñoPed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAñoPed.Location = New System.Drawing.Point(80, 40)
        Me.lblAñoPed.Name = "lblAñoPed"
        Me.lblAñoPed.Size = New System.Drawing.Size(80, 21)
        Me.lblAñoPed.TabIndex = 325
        Me.lblAñoPed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHorario
        '
        Me.lblHorario.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblHorario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHorario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHorario.Location = New System.Drawing.Point(232, 8)
        Me.lblHorario.Name = "lblHorario"
        Me.lblHorario.Size = New System.Drawing.Size(160, 21)
        Me.lblHorario.TabIndex = 324
        Me.lblHorario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(176, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(48, 14)
        Me.Label18.TabIndex = 323
        Me.Label18.Text = "Horario :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 14)
        Me.Label4.TabIndex = 318
        Me.Label4.Text = "Cliente :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContrato
        '
        Me.lblContrato.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContrato.Location = New System.Drawing.Point(80, 5)
        Me.lblContrato.Name = "lblContrato"
        Me.lblContrato.Size = New System.Drawing.Size(80, 21)
        Me.lblContrato.TabIndex = 317
        Me.lblContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFolio
        '
        Me.lblFolio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblFolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFolio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolio.Location = New System.Drawing.Point(40, 72)
        Me.lblFolio.Name = "lblFolio"
        Me.lblFolio.Size = New System.Drawing.Size(24, 21)
        Me.lblFolio.TabIndex = 316
        Me.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(80, 136)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(80, 21)
        Me.lblStatus.TabIndex = 314
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(16, 139)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(43, 14)
        Me.Label15.TabIndex = 315
        Me.Label15.Text = "Status :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(16, 75)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 14)
        Me.Label16.TabIndex = 313
        Me.Label16.Text = "Folio:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(1424, 616)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 14)
        Me.Label12.TabIndex = 308
        Me.Label12.Text = "Mano de Obra:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1424, 648)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 14)
        Me.Label6.TabIndex = 307
        Me.Label6.Text = "Monto:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1424, 552)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 14)
        Me.Label11.TabIndex = 301
        Me.Label11.Text = "Status:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(1424, 520)
        Me.Label30.Name = "Label30"
        Me.Label30.TabIndex = 299
        Me.Label30.Text = "Num. Presupuesto:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAyudante
        '
        Me.lblAyudante.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAyudante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAyudante.Location = New System.Drawing.Point(176, 136)
        Me.lblAyudante.Name = "lblAyudante"
        Me.lblAyudante.Size = New System.Drawing.Size(216, 21)
        Me.lblAyudante.TabIndex = 298
        Me.lblAyudante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTecnico
        '
        Me.lblTecnico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTecnico.Location = New System.Drawing.Point(176, 88)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(216, 21)
        Me.lblTecnico.TabIndex = 297
        Me.lblTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(176, 120)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(55, 14)
        Me.Label42.TabIndex = 296
        Me.Label42.Text = "Ayudante:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(176, 64)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(46, 14)
        Me.Label39.TabIndex = 295
        Me.Label39.Text = "Técnico:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnidad
        '
        Me.lblUnidad.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblUnidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidad.Location = New System.Drawing.Point(232, 40)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(160, 21)
        Me.lblUnidad.TabIndex = 292
        Me.lblUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFAtencion
        '
        Me.lblFAtencion.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblFAtencion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFAtencion.Location = New System.Drawing.Point(80, 104)
        Me.lblFAtencion.Name = "lblFAtencion"
        Me.lblFAtencion.Size = New System.Drawing.Size(80, 21)
        Me.lblFAtencion.TabIndex = 290
        Me.lblFAtencion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(176, 40)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(39, 14)
        Me.Label41.TabIndex = 288
        Me.Label41.Text = "Unidad"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(16, 107)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(61, 14)
        Me.Label40.TabIndex = 287
        Me.Label40.Text = "F Atención:"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(452, -25)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(80, 25)
        Me.Label28.TabIndex = 281
        Me.Label28.Text = "M3568791"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(324, -25)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(33, 14)
        Me.Label26.TabIndex = 273
        Me.Label26.Text = "Serie:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tpEquipoCliente
        '
        Me.tpEquipoCliente.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpEquipoCliente.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdEquipoCliente})
        Me.tpEquipoCliente.Location = New System.Drawing.Point(4, 22)
        Me.tpEquipoCliente.Name = "tpEquipoCliente"
        Me.tpEquipoCliente.Size = New System.Drawing.Size(992, 182)
        Me.tpEquipoCliente.TabIndex = 0
        Me.tpEquipoCliente.Text = "Equipo Cliente"
        '
        'grdEquipoCliente
        '
        Me.grdEquipoCliente.DataMember = ""
        Me.grdEquipoCliente.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdEquipoCliente.Location = New System.Drawing.Point(8, 8)
        Me.grdEquipoCliente.Name = "grdEquipoCliente"
        Me.grdEquipoCliente.ReadOnly = True
        Me.grdEquipoCliente.Size = New System.Drawing.Size(1008, 176)
        Me.grdEquipoCliente.TabIndex = 0
        Me.grdEquipoCliente.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSEquipoCliente})
        '
        'DGTSEquipoCliente
        '
        Me.DGTSEquipoCliente.DataGrid = Me.grdEquipoCliente
        Me.DGTSEquipoCliente.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBNumEquipo, Me.DGBCSerie, Me.DGBCFFabricacion, Me.DGBCEquipo, Me.DGBCTipoPropiedad, Me.DGBCCliente, Me.DGBCCelula})
        Me.DGTSEquipoCliente.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSEquipoCliente.MappingName = "equipocliente"
        '
        'DGTBNumEquipo
        '
        Me.DGTBNumEquipo.Format = ""
        Me.DGTBNumEquipo.FormatInfo = Nothing
        Me.DGTBNumEquipo.HeaderText = "NúmEquipo"
        Me.DGTBNumEquipo.MappingName = "numequipo"
        Me.DGTBNumEquipo.Width = 75
        '
        'DGBCSerie
        '
        Me.DGBCSerie.Format = ""
        Me.DGBCSerie.FormatInfo = Nothing
        Me.DGBCSerie.HeaderText = "Serie"
        Me.DGBCSerie.MappingName = "Serie"
        Me.DGBCSerie.Width = 105
        '
        'DGBCFFabricacion
        '
        Me.DGBCFFabricacion.Format = ""
        Me.DGBCFFabricacion.FormatInfo = Nothing
        Me.DGBCFFabricacion.HeaderText = "FFabricación"
        Me.DGBCFFabricacion.MappingName = "FFabricacion"
        Me.DGBCFFabricacion.Width = 75
        '
        'DGBCEquipo
        '
        Me.DGBCEquipo.Format = ""
        Me.DGBCEquipo.FormatInfo = Nothing
        Me.DGBCEquipo.HeaderText = "Equipo"
        Me.DGBCEquipo.MappingName = "Equipo"
        Me.DGBCEquipo.Width = 200
        '
        'DGBCTipoPropiedad
        '
        Me.DGBCTipoPropiedad.Format = ""
        Me.DGBCTipoPropiedad.FormatInfo = Nothing
        Me.DGBCTipoPropiedad.HeaderText = "Tipo Propiedad"
        Me.DGBCTipoPropiedad.MappingName = "TipoPropiedad"
        Me.DGBCTipoPropiedad.Width = 200
        '
        'DGBCCliente
        '
        Me.DGBCCliente.Format = ""
        Me.DGBCCliente.FormatInfo = Nothing
        Me.DGBCCliente.HeaderText = "Cliente"
        Me.DGBCCliente.MappingName = "Cliente"
        Me.DGBCCliente.Width = 0
        '
        'DGBCCelula
        '
        Me.DGBCCelula.Format = ""
        Me.DGBCCelula.FormatInfo = Nothing
        Me.DGBCCelula.HeaderText = "Célula"
        Me.DGBCCelula.MappingName = "celula"
        Me.DGBCCelula.Width = 0
        '
        'tpOrdenAutomatica
        '
        Me.tpOrdenAutomatica.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpOrdenAutomatica.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblFormaPago, Me.Label19, Me.lblFolioPresupuesto, Me.lblStatusPresupuesto, Me.lblTrabajoRealizado, Me.Label29, Me.lblTipoServicio, Me.lblUsuario, Me.txtTrabSolc, Me.Label22, Me.Label33, Me.Label34, Me.lblAutoPedido, Me.Label53, Me.txtTrabajoSolicitado, Me.Label21, Me.txtObservacionesPres, Me.Label35, Me.Label36, Me.Label37, Me.lblTot, Me.lblDesc, Me.Label44, Me.Label45, Me.lblSubT, Me.Label49, Me.lblPedido, Me.Label32})
        Me.tpOrdenAutomatica.Location = New System.Drawing.Point(4, 22)
        Me.tpOrdenAutomatica.Name = "tpOrdenAutomatica"
        Me.tpOrdenAutomatica.Size = New System.Drawing.Size(992, 182)
        Me.tpOrdenAutomatica.TabIndex = 5
        Me.tpOrdenAutomatica.Text = "Orden Automática"
        '
        'lblFormaPago
        '
        Me.lblFormaPago.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblFormaPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFormaPago.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormaPago.Location = New System.Drawing.Point(824, 8)
        Me.lblFormaPago.Name = "lblFormaPago"
        Me.lblFormaPago.Size = New System.Drawing.Size(152, 21)
        Me.lblFormaPago.TabIndex = 370
        Me.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(752, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(68, 14)
        Me.Label19.TabIndex = 369
        Me.Label19.Text = "Forma Pago:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFolioPresupuesto
        '
        Me.lblFolioPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblFolioPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFolioPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolioPresupuesto.Location = New System.Drawing.Point(312, 16)
        Me.lblFolioPresupuesto.Name = "lblFolioPresupuesto"
        Me.lblFolioPresupuesto.Size = New System.Drawing.Size(80, 21)
        Me.lblFolioPresupuesto.TabIndex = 368
        Me.lblFolioPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusPresupuesto
        '
        Me.lblStatusPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblStatusPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusPresupuesto.Location = New System.Drawing.Point(312, 48)
        Me.lblStatusPresupuesto.Name = "lblStatusPresupuesto"
        Me.lblStatusPresupuesto.Size = New System.Drawing.Size(80, 21)
        Me.lblStatusPresupuesto.TabIndex = 367
        Me.lblStatusPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTrabajoRealizado
        '
        Me.lblTrabajoRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.lblTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrabajoRealizado.Location = New System.Drawing.Point(8, 128)
        Me.lblTrabajoRealizado.Multiline = True
        Me.lblTrabajoRealizado.Name = "lblTrabajoRealizado"
        Me.lblTrabajoRealizado.ReadOnly = True
        Me.lblTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblTrabajoRealizado.Size = New System.Drawing.Size(224, 40)
        Me.lblTrabajoRealizado.TabIndex = 366
        Me.lblTrabajoRealizado.Text = ""
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(8, 112)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(93, 14)
        Me.Label29.TabIndex = 365
        Me.Label29.Text = "Trabajo Realizado"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTipoServicio
        '
        Me.lblTipoServicio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTipoServicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTipoServicio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoServicio.Location = New System.Drawing.Point(656, 72)
        Me.lblTipoServicio.Name = "lblTipoServicio"
        Me.lblTipoServicio.Size = New System.Drawing.Size(320, 21)
        Me.lblTipoServicio.TabIndex = 362
        Me.lblTipoServicio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUsuario
        '
        Me.lblUsuario.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblUsuario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(656, 40)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(320, 21)
        Me.lblUsuario.TabIndex = 361
        Me.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTrabSolc
        '
        Me.txtTrabSolc.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabSolc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabSolc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabSolc.Location = New System.Drawing.Point(576, 120)
        Me.txtTrabSolc.Multiline = True
        Me.txtTrabSolc.Name = "txtTrabSolc"
        Me.txtTrabSolc.ReadOnly = True
        Me.txtTrabSolc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabSolc.Size = New System.Drawing.Size(400, 40)
        Me.txtTrabSolc.TabIndex = 360
        Me.txtTrabSolc.Text = ""
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(576, 104)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(94, 14)
        Me.Label22.TabIndex = 359
        Me.Label22.Text = "Trabajo Solicitado"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(576, 48)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(46, 14)
        Me.Label33.TabIndex = 354
        Me.Label33.Text = "Usuario:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(576, 19)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(42, 14)
        Me.Label34.TabIndex = 353
        Me.Label34.Text = "Pedido:"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAutoPedido
        '
        Me.lblAutoPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAutoPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAutoPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoPedido.Location = New System.Drawing.Point(656, 11)
        Me.lblAutoPedido.Name = "lblAutoPedido"
        Me.lblAutoPedido.Size = New System.Drawing.Size(80, 21)
        Me.lblAutoPedido.TabIndex = 352
        Me.lblAutoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(576, 80)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(72, 14)
        Me.Label53.TabIndex = 349
        Me.Label53.Text = "Tipo Servicio:"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTrabajoSolicitado
        '
        Me.txtTrabajoSolicitado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabajoSolicitado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoSolicitado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoSolicitado.Location = New System.Drawing.Point(8, 56)
        Me.txtTrabajoSolicitado.Multiline = True
        Me.txtTrabajoSolicitado.Name = "txtTrabajoSolicitado"
        Me.txtTrabajoSolicitado.ReadOnly = True
        Me.txtTrabajoSolicitado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoSolicitado.Size = New System.Drawing.Size(224, 48)
        Me.txtTrabajoSolicitado.TabIndex = 348
        Me.txtTrabajoSolicitado.Text = ""
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(8, 40)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 14)
        Me.Label21.TabIndex = 347
        Me.Label21.Text = "TrabajoSolicitado"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtObservacionesPres
        '
        Me.txtObservacionesPres.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtObservacionesPres.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesPres.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesPres.Location = New System.Drawing.Point(408, 32)
        Me.txtObservacionesPres.Multiline = True
        Me.txtObservacionesPres.Name = "txtObservacionesPres"
        Me.txtObservacionesPres.ReadOnly = True
        Me.txtObservacionesPres.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesPres.Size = New System.Drawing.Size(152, 128)
        Me.txtObservacionesPres.TabIndex = 346
        Me.txtObservacionesPres.Text = ""
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(408, 8)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(141, 14)
        Me.Label35.TabIndex = 345
        Me.Label35.Text = "Observaciones Presupuesto"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(240, 120)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(61, 14)
        Me.Label36.TabIndex = 344
        Me.Label36.Text = "Descuento:"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(240, 152)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(33, 14)
        Me.Label37.TabIndex = 343
        Me.Label37.Text = "Total:"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTot
        '
        Me.lblTot.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTot.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTot.Location = New System.Drawing.Point(312, 144)
        Me.lblTot.Name = "lblTot"
        Me.lblTot.Size = New System.Drawing.Size(80, 21)
        Me.lblTot.TabIndex = 342
        Me.lblTot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDesc
        '
        Me.lblDesc.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDesc.Location = New System.Drawing.Point(312, 112)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(80, 21)
        Me.lblDesc.TabIndex = 341
        Me.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(240, 56)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(43, 14)
        Me.Label44.TabIndex = 340
        Me.Label44.Text = "Status :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(240, 24)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(76, 14)
        Me.Label45.TabIndex = 339
        Me.Label45.Text = "Num. Presup.:"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSubT
        '
        Me.lblSubT.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblSubT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubT.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubT.Location = New System.Drawing.Point(312, 80)
        Me.lblSubT.Name = "lblSubT"
        Me.lblSubT.Size = New System.Drawing.Size(80, 21)
        Me.lblSubT.TabIndex = 336
        Me.lblSubT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(240, 88)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(52, 14)
        Me.Label49.TabIndex = 335
        Me.Label49.Text = "SubTotal:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPedido
        '
        Me.lblPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedido.Location = New System.Drawing.Point(80, 8)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(152, 21)
        Me.lblPedido.TabIndex = 332
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(16, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(32, 14)
        Me.Label32.TabIndex = 329
        Me.Label32.Text = "Folio:"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.Catalogos, Me.Liquidacion})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.MenuItem5, Me.MenuItem6, Me.MenuItem8, Me.MenuItem12, Me.MenuItem7})
        Me.MenuItem1.MergeOrder = 50
        Me.MenuItem1.Text = "&Programación"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "&ReProgramar"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.Text = "&CerrarOrden"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.Text = "&Buscar"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem9, Me.MenuItem10, Me.MenuItem11, Me.MenuItem13, Me.MenuItem14, Me.MenuItem15, Me.MenuItem19})
        Me.MenuItem5.Text = "&Imprimir"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 0
        Me.MenuItem9.Text = "Orden de Servicio"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "Programación"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 2
        Me.MenuItem11.Text = "Presupuesto"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 3
        Me.MenuItem13.Text = "Solo una Orden de Servicio"
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 4
        Me.MenuItem14.Text = "Solo un Presupuesto"
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 5
        Me.MenuItem15.Text = "Pagares"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 6
        Me.MenuItem19.Text = "Remisiones"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 4
        Me.MenuItem6.Text = "&Refrescar"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 5
        Me.MenuItem8.Text = "&Reportes"
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 6
        Me.MenuItem12.Text = "-"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 7
        Me.MenuItem7.Text = "&Cerrar"
        '
        'Catalogos
        '
        Me.Catalogos.Index = 1
        Me.Catalogos.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem17, Me.MenuItem18})
        Me.Catalogos.Text = "Catálogos"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 0
        Me.MenuItem17.Text = "Material"
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = 1
        Me.MenuItem18.Text = "xxx"
        '
        'Liquidacion
        '
        Me.Liquidacion.Index = 2
        Me.Liquidacion.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem16, Me.MenuItem20})
        Me.Liquidacion.Text = "Liquidación"
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 0
        Me.MenuItem16.Text = "Captura Liquidación"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 1
        Me.MenuItem20.Text = "dfdf"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(648, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 24)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "FProgramacion:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(856, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Célula:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCelula
        '
        Me.cboCelula.Location = New System.Drawing.Point(904, 8)
        Me.cboCelula.Name = "cboCelula"
        Me.cboCelula.Size = New System.Drawing.Size(88, 21)
        Me.cboCelula.TabIndex = 13
        '
        'lblStatusServicio
        '
        Me.lblStatusServicio.Enabled = False
        Me.lblStatusServicio.Location = New System.Drawing.Point(624, 8)
        Me.lblStatusServicio.Name = "lblStatusServicio"
        Me.lblStatusServicio.Size = New System.Drawing.Size(24, 24)
        Me.lblStatusServicio.TabIndex = 14
        '
        'lblPuntos
        '
        Me.lblPuntos.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPuntos.Location = New System.Drawing.Point(632, 368)
        Me.lblPuntos.Name = "lblPuntos"
        Me.lblPuntos.Size = New System.Drawing.Size(72, 16)
        Me.lblPuntos.TabIndex = 15
        Me.lblPuntos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(552, 368)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 16)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Total Puntos:"
        '
        'frmServProgramacion1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(994, 601)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label9, Me.lblPuntos, Me.lblStatusServicio, Me.cboCelula, Me.Label2, Me.Label1, Me.tbLlenaServicioTecnico, Me.dtpFecha, Me.grdProgramacion, Me.ToolBar1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmServProgramacion1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Programacion de servicios técnicos"
        CType(Me.grdProgramacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbLlenaServicioTecnico.ResumeLayout(False)
        Me.tpCliente.ResumeLayout(False)
        Me.tpOrdenServicio.ResumeLayout(False)
        Me.tpServicioTecnico.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.tpEquipoCliente.ResumeLayout(False)
        CType(Me.grdEquipoCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpOrdenAutomatica.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private sda As SqlDataAdapter
    Private programacion As DataSet
    Private dthistorico As DataTable
    Private dtOrden As DataTable
    Private dtequipocliente As DataTable
    Private dtcliente As DataTable
    Private dvhistorico As DataView
    Private dvequipocliente As DataView
    Private dvcliente As DataView
    Private servicio As DataTable
    'se encarga de llenar los datos en el grid
    Private Sub CargaDatos()
        Me.grdProgramacion.DataSource = Nothing
        Dim da As New SqlDataAdapter("Select PedidoReferencia,Cliente,Ruta,FPedido," _
                         & "        FCompromiso,Usuario,Status," _
                         & "        TipoServicio,Puntos,TipoCobro,Tecnico " _
                         & "  From  vwSTLlenaProgranmacion " _
                         & "  Where tipocargo = 2 " _
                         & "    And fcompromiso >= '" & dtpFecha.Value.ToShortDateString & " 00:00:00' " _
                         & "    And Fcompromiso <= '" & dtpFecha.Value.ToShortDateString & " 23:59:59' " _
                         & "    And celula = '" & cboCelula.Text & " ' ", cnnSigamet)
        Dim dt As New DataTable("Programacion")
        da.Fill(dt)
        Me.grdProgramacion.DataSource = dt
    End Sub

    Private Sub LlenaPagare()
        Try
            Dim daLlenaPagare As New SqlDataAdapter("select formapago,numeropagos,parcialidad,frecuencia from  vwSTLlenaFormaPagoCerrarOrden where pedido = " & lblFolio.Text & "and celula = " & lblCelula.Text & "and añoped = " & lblAñoPed.Text, cnnSigamet)
            Dim dtLLenaPagare As New DataTable("Pagare")
            daLlenaPagare.Fill(dtLLenaPagare)
            If dtLLenaPagare.Rows.Count <> 0 Then
                lblTipoPedido.Text = CType(dtLLenaPagare.Rows(0).Item("formapago"), String)
                lblNumPagos.Text = CType(dtLLenaPagare.Rows(0).Item("numeropagos"), String)
                lblParcialidad.Text = CType(dtLLenaPagare.Rows(0).Item("parcialidad"), String)
                lblDias.Text = CType(dtLLenaPagare.Rows(0).Item("frecuencia"), String)
            Else
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        'Dim daLlenaPagare As New SqlDataAdapter("select formapago,numeropagos,parcialidad,frecuencia from  vwSTLlenaFormaPagoCerrarOrden where pedido = " & lblFolio.Text & "and celula = " & lblCelula.Text & "and añoped = " & lblAñoPed.Text, cnnSigamet)
        'Dim dtLLenaPagare As New DataTable("Pagare")
        'daLlenaPagare.Fill(dtLLenaPagare)
        'If dtLLenaPagare.Rows.Count <> 0 Then
        '    lblTipoPedido.Text = CType(dtLLenaPagare.Rows(0).Item("formapago"), String)
        '    lblNumPagos.Text = CType(dtLLenaPagare.Rows(0).Item("numeropagos"), String)
        '    lblParcialidad.Text = CType(dtLLenaPagare.Rows(0).Item("parcialidad"), String)
        '    lblDias.Text = CType(dtLLenaPagare.Rows(0).Item("frecuencia"), String)
        'Else
        'End If
    End Sub


    Private Sub frmServProgramacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cboCelula.Text = "1"
        'CargaDatos()

        Iniciado = True
        llena()
        CargaDatos()
        dtpFecha.Value = Now.Date.AddDays(1)
        cboCelula.Text = "1"
        lblStatusServicio.Visible = False
        lblFolio.Visible = False
        SumaPuntos()
        lblPuntos.Text = CType(suma, String)

        Cursor = Cursors.WaitCursor
        If Now.DayOfWeek = DayOfWeek.Saturday Then
            Dim f As Form
            For Each f In Me.MdiChildren
                If f.Name = "frmServProgramacion" Then
                    f.Focus()
                    Cursor = Cursors.Default
                    Exit Sub
                End If
            Next

            dtpFecha.Value = Now.Date.AddDays(2)

        Else
            Dim f As Form
            For Each f In Me.MdiChildren
                If f.Name = "frmServProgramacion" Then
                    f.Focus()
                    Cursor = Cursors.Default
                    Exit Sub
                End If
            Next

            dtpFecha.Value = Now.Date.AddDays(1)

        End If
        Cursor = Cursors.Default

    End Sub


    Public Sub Entrar(ByVal Reporte As Integer)
        Dim SQLConnection As SqlConnection = SigaMetClasses.DataLayer.Conexion
        Dim dsReporte As New DataSet()
        Dim dtReporte As DataTable = Nothing

        Try

            SQLConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.ToString, MsgBoxStyle.OKOnly, "Mensaje del sistema")
        Finally
            SQLConnection.Close()
            'SQLConnection.Dispose()
        End Try

    End Sub



    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text

            Case "Reprogramar"
                Application.DoEvents()
                Dim ReProgramar As New frmReProgramar(CType(lblFolio.Text, Integer), CType(lblCelula.Text, Integer), CType(lblAñoPed.Text, Integer), GLOBAL_Usuario)
                ReProgramar.lblPedidoReferencia.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 0), String)
                ReProgramar.dtpFCompromiso.Value = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 4), Date)
                ReProgramar.lblCliente.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 1), String)
                'ReProgramar.lblAñoPed.Text = lblAñoPed.Text
                'ReProgramar.lblcelula.Text = lblCelula.Text
                'ReProgramar.lblPedido.Text = lblFolio.Text()
                ReProgramar.lblCliente.Visible = False
                ReProgramar.lblAñoPed.Visible = False
                ReProgramar.ShowDialog()
                CargaDatos()
            Case "Asignar"
                Cursor = Cursors.WaitCursor
                Dim Asignar As New frmAsignar(CType(lblFolio.Text, Integer), CType(lblCelula.Text, Integer), CType(lblAñoPed.Text, Integer), FCompromiso, GLOBAL_Usuario)
                Asignar.lblPedidoReferencia.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 0), String)

                Asignar.ShowDialog()
                Cursor = Cursors.Default
                CargaDatos()
            Case "Liquidar"

                'Cursor = Cursors.WaitCursor
                'Dim Liquidacion As New frmSelecLiquidacion(GLOBAL_Usuario)
                'Liquidacion.ShowDialog()
                'Cursor = Cursors.Default

                Cursor = Cursors.WaitCursor
                Dim liquidacion As New LiquidacionST(GLOBAL_Usuario)
                liquidacion.ShowDialog()
                Cursor = Cursors.Default
            Case "Canc.Liquidación"
                Cursor = Cursors.WaitCursor
                Dim CancelaLiquidacion As New FrmCancelarLiquidacion(GLOBAL_Usuario)
                CancelaLiquidacion.ShowDialog()
                Cursor = Cursors.Default

            Case "Cancelar"
                StatusST = RTrim(CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 6), String))
                If StatusST = "ATENDIDO" Then
                    MessageBox.Show("No puede cancelar un servicio tecnico, con status atendido", "Servicos Técnicos", MessageBoxButtons.OK)
                Else
                    If StatusST = "CANCELADO" Then
                        MessageBox.Show("No puede cancelar un servicio tecnico, con status cancelado", "Servicos Técnicos", MessageBoxButtons.OK)
                    Else
                        Cursor = Cursors.WaitCursor
                        Dim CancelarOrden As New frmCancelarOrden(CType(lblFolio.Text, Integer), CType(lblCelula.Text, Integer), CType(lblAñoPed.Text, Integer), GLOBAL_Usuario)
                        CancelarOrden.ShowDialog()
                        Cursor = Cursors.Default
                        CargaDatos()
                    End If

                End If

            Case "Imprimir"
                Try
                    Dim folio As Integer
                    folio = 0
                    Dim Reportes As New frmReporte(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
                    reportes.Imprime = 1
                    Reportes.ShowDialog()
                Catch er As Exception
                    MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

                End Try
                'Imprime()


            Case "Refrescar"
                Application.DoEvents()
                llena()
                CargaDatos()

            Case "Reportes"

                Try
                    Dim folio As Integer
                    folio = 0
                    Dim Reportes As New frmReporte(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
                    reportes.Imprime = 1
                    Reportes.ShowDialog()
                Catch er As Exception
                    MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

                End Try

            Case "Ciclos"

                Dim CerrarCiclos As New frmCerrarCiclos()
                CerrarCiclos.ShowDialog()

            Case "Cerrar"
                If MessageBox.Show("¿Desea salir de Programación?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If
        End Select


    End Sub


    Private Sub dbgrdProgramacion_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgramacion.CurrentCellChanged

        grdProgramacion.Select(grdProgramacion.CurrentCell.RowNumber)
        'RefrescaDetalle()
        lblContrato.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 1), String)
        lblCliente.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 1), String)
        lblFolio.Text = RTrim(CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 0), String))
        FCompromiso = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 4), Date)
        LlenaServiciosTecnicos()
        CargaDatosCliente()
        LlenaPestañaPresupuesto()
        ActualizaObservaciones()
        llenaClasificacionCliente()
        OrdenAutomatica()
        LlenaPagare()
    End Sub

    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem7.Click
        Me.Close()
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        If Iniciado = True Then
            CargaDatos()
            SumaPuntos()
            lblPuntos.Text = CType(suma, String)
        End If
    End Sub

    Private Sub tpServicioTecnico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpServicioTecnico.Click
        lblContrato.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 0), String)

        LlenaServiciosTecnicos()

    End Sub



    Private Sub cboCelula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCelula.SelectedIndexChanged
        CargaDatos()
        SumaPuntos()
        lblPuntos.Text = CType(suma, String)
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Application.DoEvents()
        Dim ReProgramar As New frmReProgramar(CType(lblFolio.Text, Integer), CType(lblCelula.Text, Integer), CType(lblAñoPed.Text, Integer), GLOBAL_Usuario)
        'ReProgramar.lblPedido.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 3), String)
        ReProgramar.dtpFCompromiso.Value = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 5), Date)
        ReProgramar.lblCliente.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 0), String)
        'ReProgramar.lblAñoPed.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 3), String)
        'ReProgramar.lblcelula.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 1), String)
        ReProgramar.lblCliente.Visible = False
        ReProgramar.lblAñoPed.Visible = False
        ReProgramar.ShowDialog()
        CargaDatos()

    End Sub


    'Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
    '    Application.DoEvents()

    '    Dim cerrarorden As New frmCerrarOrden(CType(lblFolio.Text, Integer), CType(lblCelula.Text, Integer), CType(lblAñoPed.Text, Integer), cobro, añocobro)

    '    cerrarorden.lblContratoCerrar.Text = CType(grdProgramacion.Item(grdProgramacion.CurrentRowIndex, 0), String)

    '    cerrarorden.ShowDialog()

    '    CargaDatos()
    'End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Dim Buscar As New SigaMetClasses.BuscaCliente(1000, True)
        If Buscar.ShowDialog() = DialogResult.OK Then
            MessageBox.Show(Buscar.ClienteSeleccionado.ToString)
        End If
    End Sub

    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click
        Application.DoEvents()
        llena()
        CargaDatos()
    End Sub


    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        Try
            Dim strQueryOrden As String = "select cliente,observaciones,nombre,pedido,fcompromiso,tiposervicio+' '+girocliente as tiposervicio,tipocargo,domiciliocompleto,entrecalle,cp,celuladescripcion,callereferencia,telcasa,colonianombre,municipionombre,ruta,statusserviciotecnico,FCompromisoIniacial from vwSTImprimeOrdendeservicio where   celula = " & cboCelula.SelectedIndex & " and fcompromiso = ' " & dtpFecha.Value & " '"
            Dim daOrden As New SqlClient.SqlDataAdapter(strQueryOrden, cnnSigamet)
            Dim dtOrden As New DataTable("ImprimeOrden")
            daOrden.Fill(dtOrden)
            Dim dr As DataRow
            For Each dr In dtOrden.Rows
                lblStatusServicio.Text = Trim(CType(dr("statusserviciotecnico"), String))
                If lblStatusServicio.Text = "ACTIVO" Then
                    Me.OrdenServicioImprime(CType(dr("cliente"), Integer), CType(dr("observaciones"), String), CType(dr("nombre"), String), CType(dr("Pedido"), Integer), CType(dr("fcompromiso"), Date), CType(dr("tiposervicio"), String), CType(dr("tipocargo"), String), CType(dr("domiciliocompleto"), String), CType(dr("entrecalle"), String), CType(dr("cp"), Integer), CType(dr("celuladescripcion"), String), CType(dr("callereferencia"), String), CType(dr("telcasa"), String), CType(dr("colonianombre"), String), CType(dr("municipionombre"), String), CType(dr("ruta"), Integer), CType(dr("FCompromisoIniacial"), DateTime))
                Else

                End If

            Next
        Catch falla As Exception
            MessageBox.Show("Error al Imprimir Orden de Servicio")
        End Try
    End Sub


    Private Sub MenuItem10_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem10.Click
        Try
            Dim folio As Integer
            folio = 0
            Dim Reportes As New frmReporte(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
            Reportes.ShowDialog()
        Catch er As Exception
            MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

        End Try

    End Sub

    Private Sub MenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem11.Click
        Try

            Dim strQuery As String = "select FolioPresupuesto,Cliente,NombreCliente,domicilioCompleto,Entrecalle,CP,CelulaDescripcion,CalleReferencia,TelCelula,TelCasa,ColoniaNombre,MunicipioNombre,Ruta,statusserviciotecnico,fcompromiso from vwSTimprimepresupuesto where tiposervicio in (4,5,6) and celula = " & cboCelula.SelectedIndex & " and fcompromiso= ' " & dtpFecha.Value & " ' "
            Dim da As New SqlClient.SqlDataAdapter(strQuery, cnnSigamet)
            Dim dt As New DataTable("impresionpresupuesto")
            da.Fill(dt)
            Dim dr As DataRow
            For Each dr In dt.Rows
                lblStatusServicio.Text = Trim(CType(dr("statusserviciotecnico"), String))
                If lblStatusServicio.Text = "ACTIVO" Then
                    Me.ImprimePresupuesto(CType(dr("foliopresupuesto"), Integer), CType(dr("cliente"), Integer), CType(dr("NombreCliente"), String), CType(dr("domiciliocompleto"), String), CType(dr("EntreCalle"), String), CType(dr("cp"), Integer), CType(dr("celuladescripcion"), String), CType(dr("CalleReferencia"), String), CType(dr("TelCelula"), String), CType(dr("telcasa"), String), CType(dr("colonianombre"), String), CType(dr("municipionombre"), String), CType(dr("ruta"), Integer), CType(dr("fcompromiso"), Date))
                End If

            Next
        Catch falla As Exception
            MessageBox.Show("Error al Imprimir Presupuesto")
        End Try

    End Sub


    Private Sub MenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem13.Click
        Try
            Dim strQueryOrden As String = "select cliente,observaciones,nombre,pedido,fcompromiso,tiposervicio+' '+girocliente as tiposervicio,tipocargo,domiciliocompleto,entrecalle,cp,celuladescripcion,callereferencia,telcasa,colonianombre,municipionombre,ruta,statusserviciotecnico,FCompromisoIniacial from vwSTImprimeOrdendeservicio where celula = " & lblCelula.Text & " and pedido = " & lblFolio.Text & " and añoped = " & lblAñoPed.Text & " "
            Dim daOrden As New SqlClient.SqlDataAdapter(strQueryOrden, cnnSigamet)
            Dim dtOrden As New DataTable("ImprimeOrden")
            daOrden.Fill(dtOrden)
            Dim dr As DataRow
            For Each dr In dtOrden.Rows
                lblStatusServicio.Text = Trim(CType(dr("statusserviciotecnico"), String))
                If lblStatusServicio.Text = "ACTIVO" Then
                    Me.OrdenServicioImprime(CType(dr("cliente"), Integer), CType(dr("observaciones"), String), CType(dr("nombre"), String), CType(dr("Pedido"), Integer), CType(dr("fcompromiso"), Date), CType(dr("tiposervicio"), String), CType(dr("tipocargo"), String), CType(dr("domiciliocompleto"), String), CType(dr("entrecalle"), String), CType(dr("cp"), Integer), CType(dr("celuladescripcion"), String), CType(dr("callereferencia"), String), CType(dr("telcasa"), String), CType(dr("colonianombre"), String), CType(dr("municipionombre"), String), CType(dr("ruta"), Integer), CType(dr("FCompromisoIniacial"), DateTime))
                Else

                End If

            Next
        Catch falla As Exception
            MessageBox.Show("Error al Imprimir")
        End Try
    End Sub


    Private Sub MenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem14.Click
        Try

            Dim strQuery As String = "select FolioPresupuesto,Cliente,NombreCliente,domicilioCompleto,Entrecalle,CP,CelulaDescripcion,CalleReferencia,TelCelula,TelCasa,ColoniaNombre,MunicipioNombre,Ruta,statusserviciotecnico,fcompromiso from vwSTimprimepresupuesto where Añoped = " & lblAñoPed.Text & "and celula = " & lblCelula.Text & " and pedido = ' " & lblFolio.Text & " ' "
            Dim da As New SqlClient.SqlDataAdapter(strQuery, cnnSigamet)
            Dim dt As New DataTable("impresionpresupuesto")
            da.Fill(dt)
            Dim dr As DataRow
            For Each dr In dt.Rows
                lblStatusServicio.Text = Trim(CType(dr("statusserviciotecnico"), String))
                If lblStatusServicio.Text = "ACTIVO" Then
                    Me.ImprimePresupuesto(CType(dr("foliopresupuesto"), Integer), CType(dr("cliente"), Integer), CType(dr("NombreCliente"), String), CType(dr("domiciliocompleto"), String), CType(dr("EntreCalle"), String), CType(dr("cp"), Integer), CType(dr("celuladescripcion"), String), CType(dr("CalleReferencia"), String), CType(dr("TelCelula"), String), CType(dr("telcasa"), String), CType(dr("colonianombre"), String), CType(dr("municipionombre"), String), CType(dr("ruta"), Integer), CType(dr("fcompromiso"), Date))
                End If

            Next
        Catch falla As Exception
            MessageBox.Show("Error al Imprimir Presupuesto")
        End Try
    End Sub



    Private Sub tpOrdenAutomatica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpOrdenAutomatica.Click

    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub MenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem15.Click
        Try
            Dim Folio As Integer
            Folio = 0
            Dim Reportes As New frmReporte(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), Folio)
            Reportes.Imprime = 2
            Reportes.ShowDialog()
        Catch er As Exception
            MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

        End Try


    End Sub

    Private Sub MenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Catalogos.Click

    End Sub

    Private Sub MenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem17.Click
        Cursor = Cursors.WaitCursor
        Dim material As New frmMaterial(GLOBAL_Usuario)
        material.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub MenuItem16_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem16.Click
        Cursor = Cursors.WaitCursor
        Dim Liquidacion As New frmSelecLiquidacion(GLOBAL_Usuario)
        Liquidacion.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub txtObserv_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtObserv.TextChanged

    End Sub

    Private Sub Label41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label41.Click

    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click

    End Sub

    Private Sub MenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem19.Click
        Try
            Dim Folio As Integer
            Folio = 0
            Dim Reportes As New frmReporte(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), Folio)
            Reportes.Imprime = 4
            Reportes.ShowDialog()
        Catch er As Exception
            MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

        End Try
    End Sub


    Private Sub grdProgramacion_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles grdProgramacion.Navigate

    End Sub
End Class
