Option Strict On
Imports System.Data.SqlClient
Public Class frmServicios1
    Inherits System.Windows.Forms.Form
    Private _Usuario As String
    Private _Clave As String
    Private _Cliente As Integer
    Private _Celula As Integer
    Private _Ruta As Integer
    Private _Tipo As Integer
    Private _Pedido As Integer
    Private _Anio As Integer
    Private sqlcliente As SqlDataAdapter
    Private catalogo As DataSet
    Private DatosCargados As Boolean = False
    Private AñoPed As Integer
    Public TipoServicio As String
    Public PedidoC As Integer
    Public CelulaC As Integer
    Public AñoPedC As Integer
    Public _PedidoReferencia As String
    Public Suma As Integer
    Public Cuenta As Integer
    Public _Material As Integer
    Public _Cantidad As Integer
    Public _SubTotal As Integer
    Public ImporteLetra As String
    Public TipoPedido As Integer
    Public Presupuesto As String
    Public Sub New(ByVal Cliente As Integer, _
                   ByVal Fecha As Date, _
                   ByVal Usuario As String)

        MyBase.New()
        InitializeComponent()

        'Seguridad del modulo
        _Usuario = Usuario
        oSeguridad = New SigaMetClasses.cSeguridad(_Usuario, 11)

        dtpFCompromiso.Value = Fecha
        CargaDatos(Cliente)
        LlenaClasificacionCliente(Cliente)

        Dim _MañanaEsDiaFestivo As Boolean
        Dim _FechaCorte As Date
        Dim _FechaCorteFin As Date
        Dim oConfig As New SigaMetClasses.cConfig(11)
        Dim _FechaDestino As Date
        Dim _FechaExtraordinaria As Date = _
            SigaMetClasses.CalculaFechaCardinal(Now.Year, _
            CType(Now.Month, SigaMetClasses.Enumeradores.enumMesAño), _
            SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, _
            SigaMetClasses.Enumeradores.enumCardinalidad.Segundo)

        Try
            ST_HoraCorte = CType(oConfig.Parametros("HoraCorteEntreSemana"), String)
            ST_HoraCorteFin = CType(oConfig.Parametros("horacortefinsemana"), String)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            oConfig.Dispose()

        End Try

        _FechaCorte = CType((Now.Date.ToShortDateString & " " & ST_HoraCorte), Date)
        _FechaCorteFin = CType(Now.Date.ToShortDateString & " " & ST_HoraCorteFin, Date)

        Dim strQuery As String = _
        "SELECT Año, Mes, Dia FROM Festivo WHERE Tipo = 'FESTIVO'"
        Dim da As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dt As New DataTable("Festivo")
        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow
                For Each dr In dt.Rows
                    If CType(dr("Dia"), Integer) = Now.Date.AddDays(1).Day And CType(dr("Mes"), Integer) = Now.Date.AddDays(1).Month Then
                        _MañanaEsDiaFestivo = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            da.Dispose()
        End Try


        If _FechaCorte > Now Then
            'Antes del corte
            If Not _MañanaEsDiaFestivo Then
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Saturday
                        If Now.Date.AddDays(1) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(1)
                        Else
                            _FechaDestino = Now.Date.AddDays(2)
                        End If

                    Case Is = DayOfWeek.Sunday
                        _FechaDestino = Now.Date.AddDays(2)
                    Case Else
                        _FechaDestino = Now.Date.AddDays(1)
                End Select
            Else
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Friday
                        If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(2)
                        Else
                            _FechaDestino = Now.Date.AddDays(3)
                        End If
                    Case Else
                        _FechaDestino = Now.Date.AddDays(2)

                End Select

            End If
        Else
            'Despues del corte
            If Not _MañanaEsDiaFestivo Then
                Select Case Now.DayOfWeek
                    Case Is = DayOfWeek.Friday
                        If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(2)
                        Else
                            _FechaDestino = Now.Date.AddDays(3)
                        End If

                    Case Is = DayOfWeek.Saturday
                        If Now.Date.AddDays(1) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(2)
                        Else
                            _FechaDestino = Now.Date.AddDays(3)
                        End If

                    Case Else
                        _FechaDestino = Now.Date.AddDays(2)
                End Select
            Else
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Thursday
                        If Now.Date.AddDays(3) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(3)
                        Else
                            _FechaDestino = Now.Date.AddDays(4)
                        End If

                    Case DayOfWeek.Friday
                        If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(3)
                        Else
                            _FechaDestino = Now.Date.AddDays(4)
                        End If
                    Case Else
                        _FechaDestino = Now.Date.AddDays(3)
                End Select

            End If

        End If

        'Cursor = Cursors.WaitCursor
        'Dim oServicio As New frmServicios(CType(lbContrato.Text, Integer), _FechaDestino)
        'oServicio.ShowDialog()
        'Cursor = Cursors.Default

        If oSeguridad.TieneAcceso("MODIFICA FECHA") Then
            dtpFCompromiso.Value = _FechaDestino
            dtpFCompromiso.MinDate = Fecha.AddMonths(-1)
        Else
            dtpFCompromiso.Value = _FechaDestino
            dtpFCompromiso.MinDate = _FechaDestino
        End If
        'dtpFCompromiso.Value = _FechaDestino

    End Sub

    Private Sub CargaDatos(ByVal intCliente As Integer)
        Dim da As New SqlDataAdapter("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                     "select Cliente,Celula,Ruta,RamoCliente,GiroCliente,Equipo,Nombre  from vwSTClienteServicioTecnico Where Cliente = " & intCliente.ToString & _
                                     " SET TRANSACTION ISOLATION LEVEL READ COMMITTED ", cnnSigamet)
        Dim dt As New DataTable("Cliente")
        da.Fill(dt)

        If dt.Rows.Count >= 0 Then
            lblContrato.Text = CType(dt.Rows(0).Item("Cliente"), String)
            lblcelula.Text = CType(dt.Rows(0).Item("Celula"), String)
            lblRuta.Text = CType(dt.Rows(0).Item("ruta"), String)
            lblRamoCliente.Text = CType(dt.Rows(0).Item("RamoCliente"), String)
            lblGiroCliente.Text = CType(dt.Rows(0).Item("girocliente"), String)
            lblNombreCliente.Text = CType(dt.Rows(0).Item("Nombre"), String)
            lblTanque.Text = CType(dt.Rows(0).Item("equipo"), String)

        End If

    End Sub

    Private Sub LlenaClasificacionCliente(ByVal intCliente As Integer)
        Dim da As New SqlDataAdapter("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                     "select c.cliente,isnull(c.clasificacioncliente,'')as clasificacioncliente,isnull(descripcion,'')as descripcion  from cliente c join clasificacioncliente cc on c.clasificacioncliente = cc.clasificacioncliente where cliente = " & intCliente.ToString & _
                                     " SET TRANSACTION ISOLATION LEVEL READ COMMITTED ", cnnSigamet)
        Dim dt As New DataTable("ClasificacionCliente")
        da.Fill(dt)
        lblClasificacionCliente.Text = CType(dt.Rows(0).Item("descripcion"), String)
    End Sub
    'SE NECESITA REVISAR PARA LLENAR LOS DATOS DE SERVICIOS
    Private Sub LlenaObservaciones()
        Dim Status As String
        _PedidoReferencia = CType(RTrim(CType(_PedidoReferencia, String)), String)
        Dim daObs As New SqlDataAdapter("select pedido,añoped,celula,tiposervicio,observaciones,ObservacionesServicioRealizado,FCompromiso,fcompromisoinciial,statusserviciotecnico from vwSTLlenaObservaciones where pedidoreferencia = '" & _PedidoReferencia & "'", cnnSigamet)
        Try

            Dim dtObs As New DataTable("observaciones")
            daObs.Fill(dtObs)
            Status = RTrim(CType(dtObs.Rows(0).Item("statusserviciotecnico"), String))
            txtObservaciones.Text = CType(dtObs.Rows(0).Item("Observaciones"), String)
            txtTrabajoRealizado.Text = CType(dtObs.Rows(0).Item("observacionesserviciorealizado"), String)
            cboTipoServicio.SelectedValue = dtObs.Rows(0).Item("tiposervicio")
            lblHoraAtencion.Text = CType(dtObs.Rows(0).Item("fcompromisoinciial"), String)
            If Status = "ACTIVO" Then
                dtpFCompromiso.Value = CType(dtObs.Rows(0).Item("FCompromiso"), Date)
            Else
            End If

            PedidoC = CType(dtObs.Rows(0).Item("pedido"), Integer)
            CelulaC = CType(dtObs.Rows(0).Item("celula"), Integer)
            AñoPedC = CType(dtObs.Rows(0).Item("añoped"), Integer)

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try



    End Sub

    Private Sub LlenaTipoPropiedad()
        Dim daTipoPropiedad As New SqlDataAdapter("select cliente,pedido,p.tipopedido,descripcion from pedido p left join tipopedido TP on p.tipopedido = tp.tipopedido where tp.tipopedido in (7,8,9,10) and cliente = " & lblContrato.Text & "and pedido = " & PedidoC & "and celula = " & lblcelula.Text & " and añoped = " & lblAñoPed.Text, cnnSigamet)
        Dim dtTipoPropiedad As New DataTable("Tipo")
        daTipoPropiedad.Fill(dtTipoPropiedad)
        cboTipoPedido.SelectedItem = dtTipoPropiedad.Rows(0).Item("tipopedido")
    End Sub

    'Private Sub LLenaPrecio()
    '    lblTotalEjecutivas.Text = CType(cboMaterial.SelectedValue, String)
    '    Dim daPrecio As New SqlDataAdapter("select Material,Descripción,precio from material where material =  " & lblTotalEjecutivas.Text, cnnSigamet)
    '    Dim dtPrecio As New DataTable("Precio")
    '    daPrecio.Fill(dtPrecio)
    '    lblTotalEjecutivas.Text = CType(dtPrecio.Rows(0).Item("precio"), String)
    'End Sub



    Private Sub LlenaCombos()

        Dim Llenacombo As String = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " _
                                   & "select tiposervicio,cast(tiposervicio as varchar (5))+' '+ts.descripcion+' '+gc.descripcion as descripcion from tiposervicio ts join girocliente gc on ts.girocliente = gc.girocliente" _
                                   & " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        Dim SqlTipoServicio As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsTipoServicio As New DataSet()
        SqlTipoServicio.Fill(dsTipoServicio, "tiposervicio")
        With cboTipoServicio
            .DataSource = dsTipoServicio.Tables("tiposervicio")
            .DisplayMember = "descripcion"
            .ValueMember = "tiposervicio"
            .SelectedIndex = 0
        End With
        Llenacombo = ""

        'llena combo material
        Llenacombo = "select material,Descripcion + '            $ ' +cast(isnull(Precio,0) as varchar (10))as descripcion FROM MATERIAL"
        Dim sqlMaterial As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsMaterial As New DataSet()
        sqlMaterial.Fill(dsMaterial, "materiales")
        With cboMaterial
            .DataSource = dsMaterial.Tables("materiales")
            .DisplayMember = "descripcion"
            .ValueMember = "material"
            .SelectedIndex = 0
        End With

        Llenacombo = ""



        'llena combo tipo de credito
        If oSeguridad.TieneAcceso("MODIFICA FECHA") Then
            Llenacombo = "select creditoserviciotecnico,descripcion from creditoserviciotecnico"
        Else
            Llenacombo = "select creditoserviciotecnico,descripcion from creditoserviciotecnico WHERE Administra = 1"
        End If

        Dim SQLTipoCredito As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsTipoCredito As New DataSet()
        SQLTipoCredito.Fill(dsTipoCredito, "TipoCredito")
        With CboTipoCredito
            .DataSource = dsTipoCredito.Tables("TipoCredito")
            .DisplayMember = "descripcion"
            .ValueMember = "creditoserviciotecnico"
            '.SelectedIndex = 0
        End With
        Try
            CboTipoCredito.SelectedIndex = 0
        Catch e As Exception
            CboTipoCredito.SelectedIndex = 0
        End Try

        Llenacombo = ""

        'Llena combo Para TipoCargo
        If oSeguridad.TieneAcceso("TIPO PROPIEDAD") Then
            Llenacombo = "select tipopedido,descripcion from tipopedido where tipopedido in (7,8,9,10) "
        Else
            Llenacombo = "select tipopedido,descripcion from tipopedido where tipopedido in (8) "
        End If

        Dim sqlTipoPedido As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsTipoPedido As New DataSet()
        sqlTipoPedido.Fill(dsTipoPedido, "tipopedido")
        With cboTipoPedido
            .DataSource = dsTipoPedido.Tables("tipopedido")
            .DisplayMember = "descripcion"
            .ValueMember = "tipopedido"
        End With
        Try
            cboTipoPedido.SelectedIndex = 1
        Catch ex As Exception
            cboTipoPedido.SelectedIndex = 0
        End Try
        Llenacombo = ""


    End Sub
    'llena el grid de Historicos de Servicios Técnicos

    Private Sub CargaDatosHistoricos()


        Dim intCliente As Integer
        intCliente = CType(lblContrato.Text, Integer)
        Me.grdHistorico.DataSource = Nothing
        Dim da As New SqlDataAdapter("select pedidoreferencia,FCompromiso,Usuario,Status,TipoServicio,Puntos  from vwSTllenaprogranmacion where tipocargo = 2 and cliente =" & intCliente.ToString, cnnSigamet)
        Dim dt As New DataTable("Historico")
        da.Fill(dt)
        Me.grdHistorico.DataSource = dt
    End Sub



    Private Sub LlenaPresupuesto()

        Dim daPresupuesto As New SqlCommand("select pedido,cliente,fPedido,FolioPresupuesto,añofoliopresupuesto,StatusPresupuesto,subtotal,descuento,total,Descuento,Total from vwSTPedidoPresupuestoServicioTecnico where pedido = " & PedidoC & " And Celula = " & CelulaC & " And Añoped = " & AñoPedC, cnnSigamet)
        Try
            If cnnSigamet.State = ConnectionState.Open Then
                cnnSigamet.Close()
            End If
            cnnSigamet.Open()
            Dim dr As SqlDataReader = daPresupuesto.ExecuteReader
            While dr.Read
                lblSubtotal.Text = CType(dr("subtotal"), String)
                lblDescuento.Text = CType(dr("descuento"), String)
                lblTotal.Text = CType(dr("Total"), String)
                lblStatusPresupuesto.Text = CType(dr("statuspresupuesto"), String)
                lblNumPresupuesto.Text = CType(dr("foliopresupuesto"), String)
                lblFPedido.Text = CType(dr("fpedido"), String)
                lblAñoFolioPresupuesto.Text = CType(dr("añofoliopresupuesto"), String)
                lblPedido.Text = CType(dr("Pedido"), String)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub


    Private Sub LlenaTecnicos()
        Dim da As New SqlDataAdapter("SELECT Pedido,Autotanque,chofer" & _
                    " from vwSTPestanaServicioTecnico " & _
                    " Where celula = " & CelulaC & _
                    " and añoped = " & AñoPedC & " And Pedido = " & PedidoC, cnnSigamet)
        Dim dt As New DataTable("ClienteServicio")
        da.Fill(dt)
        txtTecnico.Text = CType(dt.Rows(0).Item("chofer"), String)
        lblCamioneta.Text = CType(dt.Rows(0).Item("autotanque"), String)

    End Sub

    Private Function SumaPuntos(ByVal Fecha As Date, _
                                ByVal Celula As Byte) As Integer
        Dim strQuery As String = _
        "SELECT TotalPuntos,0 FROM vwSTSumaPuntos " & _
        "where producto = 4 and fcompromiso = '" & Fecha.ToShortDateString & "' and celula = " & Celula.ToString

        Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtSumaPuntos As New DataTable("Suma")
        daSumaPuntos.Fill(dtSumaPuntos)
        If dtSumaPuntos.Rows.Count > 0 Then
            lblPuntos.Text = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), String)
        Else
        End If
        'lblPuntos.Text = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), String)
        If dtSumaPuntos.Rows.Count > 0 Then
            Return CType(dtSumaPuntos.Rows(0).Item("TotalPuntos"), Integer)
        Else
            Return 0
        End If

    End Function



    Private Sub StatusPresupuesto()

        Dim consulta As String
        consulta = "select statuspresupuesto from vwSTStatusPresupuesto where cliente = " & lblContrato.Text
        Dim daStatus As New SqlDataAdapter(consulta, cnnSigamet)
        Dim dtStatus As New DataTable("Status")
        daStatus.Fill(dtStatus)
        If dtStatus.Rows.Count > 0 Then
            MessageBox.Show("Usted No Puede Levantar un Nuevo Presupuesto, Ya que tiene Presupuestos Pendientes", "Mensaje de Servicios Técnicos")
            btnAceptar.Enabled = True
        Else

        End If

    End Sub

    Private Sub ActivaCreditos()
        Dim Tiposervicio As Integer = CType(cboTipoServicio.SelectedValue, Integer)
        If Tiposervicio = 10 And Tiposervicio = 11 And Tiposervicio = 12 And Tiposervicio = 13 And Tiposervicio = 14 And Tiposervicio = 15 Then
            GBFormaPago.Enabled = True
        Else
        End If
    End Sub


    Private Sub llenaCreditos()
        Dim TipoCredito As Integer
        TipoCredito = CType(CboTipoCredito.SelectedValue, Integer)
        Dim Consulta As String
        Consulta = "select creditoserviciotecnico,descripcion, NumeroPagos,frecuencia from creditoserviciotecnico where descripcion = ' " & TipoCredito & " '"
        Dim da As New SqlDataAdapter(Consulta, cnnSigamet)
        Dim dt As New DataTable("Pagare")
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            lblNumPagos.Text = CType(dt.Rows(0).Item("numeropagos"), String)
            lblTotal.Text = CType(dt.Rows(0).Item("frecuencia"), String)
        End If


        If lblTotal.Text > "" Then

            lblPagosde.Text = CType(CType(lblTotal.Text, Double) / 3, String)
        End If
    End Sub

    Private Sub InsertaMaterial()
        Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
        Conexion.Open()
        Dim Comando As New SqlCommand()
        Dim Transaccion As SqlTransaction
        Comando.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
        Comando.Parameters.Add("@Celula", SqlDbType.Int).Value = lblcelula.Text
        Comando.Parameters.Add("@Material", SqlDbType.Int).Value = _Material

        Comando.Parameters.Add("@SubTotal", SqlDbType.Money).Value = _SubTotal
        Transaccion = Conexion.BeginTransaction
        Comando.Connection = Conexion
        Comando.Transaction = Transaccion
        Try
            Comando.CommandType = CommandType.StoredProcedure
            Comando.CommandText = "spSTInsertaMaterialServicioTecnico"
            Comando.CommandTimeout = 300
            Comando.ExecuteNonQuery()
            Transaccion.Commit()
        Catch e As Exception
            Transaccion.Rollback()
            MessageBox.Show(e.Message)
        Finally
            Conexion.Close()
            'Conexion.Dispose()
        End Try

    End Sub



#Region " Windows Form Designer generated code "


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
    Friend WithEvents daProducto As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCProducto As System.Data.SqlClient.SqlCommand
    Friend WithEvents daHorario As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCHorario As System.Data.SqlClient.SqlCommand
    Friend WithEvents daPrioridad As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCPrioridad As System.Data.SqlClient.SqlCommand
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblStatus1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tmHora As System.Windows.Forms.Timer
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblRamoCliente As System.Windows.Forms.Label
    Friend WithEvents lblGiroCliente As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents lblTanque As System.Windows.Forms.Label
    Friend WithEvents lblNumPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblStatusPresupuesto As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents btnPresupuesto As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblEtiqueta As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents ClienteEquipo As STClienteEquipo.ClienteEquipo
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblContrato As System.Windows.Forms.Label
    Friend WithEvents lblcelula As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DGTSHistorico As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCFCompromiso As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFUsuario As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoServicio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPuntos As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents txtTecnico As System.Windows.Forms.Label
    Friend WithEvents btnProgramacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblCamioneta As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblClasificacionCliente As System.Windows.Forms.Label
    Friend WithEvents btnHorario As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblHoraAtencion As System.Windows.Forms.Label
    Friend WithEvents dtpFCompromiso As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboTipoServicio As System.Windows.Forms.ComboBox
    Friend WithEvents cboTipoPedido As System.Windows.Forms.ComboBox
    Friend WithEvents grdHistorico As System.Windows.Forms.DataGrid
    Friend WithEvents tbServicio As System.Windows.Forms.ToolBar
    Friend WithEvents lblFPedido As System.Windows.Forms.Label
    Friend WithEvents lblStatusServicioTecnico As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents txtDireccionInstalacion As System.Windows.Forms.TextBox
    Friend WithEvents lblAñoFolioPresupuesto As System.Windows.Forms.Label
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents lblTotalEjecutivas As System.Windows.Forms.Label
    Friend WithEvents GBFormaPago As System.Windows.Forms.GroupBox
    Friend WithEvents lblPuntos As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblAñoPed As System.Windows.Forms.Label
    Protected WithEvents lstMaterial As System.Windows.Forms.ListBox
    Friend WithEvents lblDias As System.Windows.Forms.Label
    Friend WithEvents lblNumPagos As System.Windows.Forms.Label
    Friend WithEvents lblPagosde As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CboTipoCredito As System.Windows.Forms.ComboBox
    Friend WithEvents DGTBCPedidoReferencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblNombreCliente As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmServicios1))
        Me.daProducto = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCProducto = New System.Data.SqlClient.SqlCommand()
        Me.daHorario = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCHorario = New System.Data.SqlClient.SqlCommand()
        Me.daPrioridad = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCPrioridad = New System.Data.SqlClient.SqlCommand()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblNombreCliente = New System.Windows.Forms.Label()
        Me.lblClasificacionCliente = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTecnico = New System.Windows.Forms.Label()
        Me.lblTecnico = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblcelula = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblContrato = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ClienteEquipo = New STClienteEquipo.ClienteEquipo()
        Me.lblTanque = New System.Windows.Forms.Label()
        Me.cboTipoPedido = New System.Windows.Forms.ComboBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.lblGiroCliente = New System.Windows.Forms.Label()
        Me.lblRamoCliente = New System.Windows.Forms.Label()
        Me.lblStatus1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboTipoServicio = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dtpFCompromiso = New System.Windows.Forms.DateTimePicker()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblFPedido = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblAñoFolioPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusServicioTecnico = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblNumPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusPresupuesto = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tmHora = New System.Windows.Forms.Timer(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.tbServicio = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnPresupuesto = New System.Windows.Forms.ToolBarButton()
        Me.btnProgramacion = New System.Windows.Forms.ToolBarButton()
        Me.btnHorario = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.grdHistorico = New System.Windows.Forms.DataGrid()
        Me.DGTSHistorico = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCPedidoReferencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCompromiso = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFUsuario = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoServicio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPuntos = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lblEtiqueta = New System.Windows.Forms.Label()
        Me.lblCamioneta = New System.Windows.Forms.Label()
        Me.lblHoraAtencion = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblAñoPed = New System.Windows.Forms.Label()
        Me.GBFormaPago = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblPagosde = New System.Windows.Forms.Label()
        Me.lblNumPagos = New System.Windows.Forms.Label()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lstMaterial = New System.Windows.Forms.ListBox()
        Me.lblTotalEjecutivas = New System.Windows.Forms.Label()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboMaterial = New System.Windows.Forms.ComboBox()
        Me.CboTipoCredito = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDireccionInstalacion = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblPuntos = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.grdHistorico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBFormaPago.SuspendLayout()
        Me.SuspendLayout()
        '
        'daProducto
        '
        Me.daProducto.SelectCommand = Me.cmdCProducto
        '
        'cmdCProducto
        '
        Me.cmdCProducto.CommandText = "SELECT Producto, Descripcion FROM Producto WHERE (TipoProducto = 2)"
        '
        'daHorario
        '
        Me.daHorario.SelectCommand = Me.cmdCHorario
        '
        'cmdCHorario
        '
        Me.cmdCHorario.CommandText = "EXEC sp_HorarioRutaGrid @Ruta, @CP"
        Me.cmdCHorario.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Ruta", System.Data.SqlDbType.Int))
        Me.cmdCHorario.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CP", System.Data.SqlDbType.Char))
        '
        'daPrioridad
        '
        Me.daPrioridad.SelectCommand = Me.cmdCPrioridad
        '
        'cmdCPrioridad
        '
        Me.cmdCPrioridad.CommandText = "SELECT PrioridadPedido, Descripcion FROM PrioridadPedido"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblNombreCliente, Me.lblClasificacionCliente, Me.Label15, Me.Label10, Me.txtTecnico, Me.lblTecnico, Me.Label5, Me.txtTrabajoRealizado, Me.lblRuta, Me.Label9, Me.lblcelula, Me.Label8, Me.lblContrato, Me.Label6, Me.ClienteEquipo, Me.lblTanque, Me.cboTipoPedido, Me.Label51, Me.lblGiroCliente, Me.lblRamoCliente, Me.lblStatus1, Me.Label7, Me.Label14, Me.txtObservaciones, Me.Label2, Me.Label3, Me.cboTipoServicio, Me.Label26, Me.Label23, Me.dtpFCompromiso, Me.Label24, Me.lblFPedido, Me.Label22})
        Me.GroupBox4.Location = New System.Drawing.Point(8, 48)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(976, 248)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Datos del Servicio Técnico"
        '
        'lblNombreCliente
        '
        Me.lblNombreCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombreCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreCliente.Location = New System.Drawing.Point(232, 21)
        Me.lblNombreCliente.Name = "lblNombreCliente"
        Me.lblNombreCliente.Size = New System.Drawing.Size(232, 21)
        Me.lblNombreCliente.TabIndex = 227
        Me.lblNombreCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClasificacionCliente
        '
        Me.lblClasificacionCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblClasificacionCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClasificacionCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClasificacionCliente.Location = New System.Drawing.Point(816, 56)
        Me.lblClasificacionCliente.Name = "lblClasificacionCliente"
        Me.lblClasificacionCliente.Size = New System.Drawing.Size(144, 21)
        Me.lblClasificacionCliente.TabIndex = 226
        Me.lblClasificacionCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(704, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 14)
        Me.Label15.TabIndex = 225
        Me.Label15.Text = "Clasificación Cliente :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(488, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 14)
        Me.Label10.TabIndex = 224
        Me.Label10.Text = "Camioneta:"
        '
        'txtTecnico
        '
        Me.txtTecnico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtTecnico.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTecnico.Location = New System.Drawing.Point(576, 120)
        Me.txtTecnico.Name = "txtTecnico"
        Me.txtTecnico.Size = New System.Drawing.Size(384, 21)
        Me.txtTecnico.TabIndex = 223
        Me.txtTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTecnico
        '
        Me.lblTecnico.AutoSize = True
        Me.lblTecnico.Location = New System.Drawing.Point(488, 123)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(50, 14)
        Me.lblTecnico.TabIndex = 222
        Me.lblTecnico.Text = "Técnico :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(248, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 14)
        Me.Label5.TabIndex = 221
        Me.Label5.Text = "Trabajo Realizado"
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(248, 168)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ReadOnly = True
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(216, 64)
        Me.txtTrabajoRealizado.TabIndex = 0
        Me.txtTrabajoRealizado.Text = ""
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(424, 120)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(40, 24)
        Me.lblRuta.TabIndex = 218
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(384, 125)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 14)
        Me.Label9.TabIndex = 219
        Me.Label9.Text = "Ruta :"
        '
        'lblcelula
        '
        Me.lblcelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblcelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblcelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcelula.Location = New System.Drawing.Point(336, 120)
        Me.lblcelula.Name = "lblcelula"
        Me.lblcelula.Size = New System.Drawing.Size(40, 24)
        Me.lblcelula.TabIndex = 216
        Me.lblcelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(288, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 217
        Me.Label8.Text = "Celula :"
        '
        'lblContrato
        '
        Me.lblContrato.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContrato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContrato.Location = New System.Drawing.Point(112, 21)
        Me.lblContrato.Name = "lblContrato"
        Me.lblContrato.Size = New System.Drawing.Size(112, 21)
        Me.lblContrato.TabIndex = 214
        Me.lblContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Cliente :"
        '
        'ClienteEquipo
        '
        Me.ClienteEquipo.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.ClienteEquipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClienteEquipo.Location = New System.Drawing.Point(488, 164)
        Me.ClienteEquipo.Name = "ClienteEquipo"
        Me.ClienteEquipo.Size = New System.Drawing.Size(472, 92)
        Me.ClienteEquipo.TabIndex = 213
        Me.ClienteEquipo.Visible = False
        '
        'lblTanque
        '
        Me.lblTanque.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTanque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTanque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTanque.Location = New System.Drawing.Point(816, 88)
        Me.lblTanque.Name = "lblTanque"
        Me.lblTanque.Size = New System.Drawing.Size(144, 21)
        Me.lblTanque.TabIndex = 212
        Me.lblTanque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTipoPedido
        '
        Me.cboTipoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoPedido.Items.AddRange(New Object() {"ACTIVO", "PROGRAMADO", "EN SERVICIO", "ATENDIDO", "CANCELADO"})
        Me.cboTipoPedido.Location = New System.Drawing.Point(112, 120)
        Me.cboTipoPedido.Name = "cboTipoPedido"
        Me.cboTipoPedido.Size = New System.Drawing.Size(168, 21)
        Me.cboTipoPedido.TabIndex = 0
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(8, 123)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(106, 14)
        Me.Label51.TabIndex = 210
        Me.Label51.Text = "TipoServicioTécnico:"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblGiroCliente
        '
        Me.lblGiroCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblGiroCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGiroCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGiroCliente.Location = New System.Drawing.Point(576, 24)
        Me.lblGiroCliente.Name = "lblGiroCliente"
        Me.lblGiroCliente.Size = New System.Drawing.Size(112, 21)
        Me.lblGiroCliente.TabIndex = 209
        Me.lblGiroCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRamoCliente
        '
        Me.lblRamoCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRamoCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRamoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRamoCliente.Location = New System.Drawing.Point(576, 56)
        Me.lblRamoCliente.Name = "lblRamoCliente"
        Me.lblRamoCliente.Size = New System.Drawing.Size(112, 21)
        Me.lblRamoCliente.TabIndex = 208
        Me.lblRamoCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus1
        '
        Me.lblStatus1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblStatus1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus1.Location = New System.Drawing.Point(816, 24)
        Me.lblStatus1.Name = "lblStatus1"
        Me.lblStatus1.Size = New System.Drawing.Size(144, 21)
        Me.lblStatus1.TabIndex = 204
        Me.lblStatus1.Text = "ACTIVO"
        Me.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(704, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 14)
        Me.Label7.TabIndex = 205
        Me.Label7.Text = "Status Servicio :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(8, 152)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(94, 14)
        Me.Label14.TabIndex = 203
        Me.Label14.Text = "Trabajo Solicitado"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservaciones.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservaciones.Location = New System.Drawing.Point(16, 168)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservaciones.Size = New System.Drawing.Size(224, 64)
        Me.txtObservaciones.TabIndex = 1
        Me.txtObservaciones.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(704, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 14)
        Me.Label2.TabIndex = 198
        Me.Label2.Text = "Tanque:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(488, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 14)
        Me.Label3.TabIndex = 197
        Me.Label3.Text = "Ramo Cliente :"
        '
        'cboTipoServicio
        '
        Me.cboTipoServicio.DisplayMember = "Descripcion"
        Me.cboTipoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoServicio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoServicio.ItemHeight = 13
        Me.cboTipoServicio.Location = New System.Drawing.Point(112, 88)
        Me.cboTipoServicio.Name = "cboTipoServicio"
        Me.cboTipoServicio.Size = New System.Drawing.Size(352, 21)
        Me.cboTipoServicio.TabIndex = 0
        Me.cboTipoServicio.ValueMember = "PrioridadPedido"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(8, 91)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 14)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "TipoServicio :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(488, 24)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(66, 14)
        Me.Label23.TabIndex = 190
        Me.Label23.Text = "Giro Cliente:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dtpFCompromiso
        '
        Me.dtpFCompromiso.CalendarTitleForeColor = System.Drawing.SystemColors.Window
        Me.dtpFCompromiso.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFCompromiso.Location = New System.Drawing.Point(112, 56)
        Me.dtpFCompromiso.Name = "dtpFCompromiso"
        Me.dtpFCompromiso.Size = New System.Drawing.Size(120, 21)
        Me.dtpFCompromiso.TabIndex = 0
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 59)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(80, 14)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "FCompromiso :"
        '
        'lblFPedido
        '
        Me.lblFPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblFPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFPedido.Location = New System.Drawing.Point(304, 56)
        Me.lblFPedido.Name = "lblFPedido"
        Me.lblFPedido.Size = New System.Drawing.Size(160, 21)
        Me.lblFPedido.TabIndex = 184
        Me.lblFPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(240, 59)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 14)
        Me.Label22.TabIndex = 185
        Me.Label22.Text = "FServicio :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblAñoFolioPresupuesto, Me.lblStatusServicioTecnico, Me.Label4, Me.Label1, Me.lblSubtotal, Me.Label12, Me.Label11, Me.Label30, Me.lblTotal, Me.lblNumPresupuesto, Me.lblStatusPresupuesto, Me.lblDescuento, Me.lblPedido, Me.Label13})
        Me.GroupBox2.Location = New System.Drawing.Point(496, 472)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(496, 128)
        Me.GroupBox2.TabIndex = 245
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos Presupuesto"
        '
        'lblAñoFolioPresupuesto
        '
        Me.lblAñoFolioPresupuesto.Location = New System.Drawing.Point(296, 152)
        Me.lblAñoFolioPresupuesto.Name = "lblAñoFolioPresupuesto"
        Me.lblAñoFolioPresupuesto.Size = New System.Drawing.Size(40, 16)
        Me.lblAñoFolioPresupuesto.TabIndex = 266
        '
        'lblStatusServicioTecnico
        '
        Me.lblStatusServicioTecnico.Location = New System.Drawing.Point(64, 112)
        Me.lblStatusServicioTecnico.Name = "lblStatusServicioTecnico"
        Me.lblStatusServicioTecnico.Size = New System.Drawing.Size(64, 8)
        Me.lblStatusServicioTecnico.TabIndex = 265
        '
        'Label4
        '
        Me.Label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(256, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 264
        Me.Label4.Text = " Total:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(256, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 14)
        Me.Label1.TabIndex = 263
        Me.Label1.Text = "SubTotal:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSubtotal
        '
        Me.lblSubtotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblSubtotal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubtotal.Location = New System.Drawing.Point(352, 24)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(128, 21)
        Me.lblSubtotal.TabIndex = 262
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(256, 56)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 14)
        Me.Label12.TabIndex = 261
        Me.Label12.Text = "Descuento:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 88)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 14)
        Me.Label11.TabIndex = 260
        Me.Label11.Text = "StatusPresupuesto:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(16, 24)
        Me.Label30.Name = "Label30"
        Me.Label30.TabIndex = 259
        Me.Label30.Text = "Num. Presupuesto:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(352, 88)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(128, 21)
        Me.lblTotal.TabIndex = 258
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumPresupuesto
        '
        Me.lblNumPresupuesto.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblNumPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumPresupuesto.Location = New System.Drawing.Point(128, 24)
        Me.lblNumPresupuesto.Name = "lblNumPresupuesto"
        Me.lblNumPresupuesto.Size = New System.Drawing.Size(112, 21)
        Me.lblNumPresupuesto.TabIndex = 257
        Me.lblNumPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusPresupuesto
        '
        Me.lblStatusPresupuesto.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblStatusPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblStatusPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusPresupuesto.Location = New System.Drawing.Point(128, 88)
        Me.lblStatusPresupuesto.Name = "lblStatusPresupuesto"
        Me.lblStatusPresupuesto.Size = New System.Drawing.Size(112, 21)
        Me.lblStatusPresupuesto.TabIndex = 256
        Me.lblStatusPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDescuento
        '
        Me.lblDescuento.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblDescuento.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblDescuento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescuento.Location = New System.Drawing.Point(352, 56)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(128, 21)
        Me.lblDescuento.TabIndex = 255
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPedido
        '
        Me.lblPedido.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedido.Location = New System.Drawing.Point(128, 56)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(112, 21)
        Me.lblPedido.TabIndex = 254
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 56)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 14)
        Me.Label13.TabIndex = 250
        Me.Label13.Text = "Pedido:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem4, Me.MenuItem5})
        Me.MenuItem1.Text = "&Servicios"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "&Aceptar"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.Text = "&Presupuesto"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "C&errar"
        '
        'tbServicio
        '
        Me.tbServicio.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbServicio.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnModificar, Me.btnPresupuesto, Me.btnProgramacion, Me.btnHorario, Me.btnCerrar})
        Me.tbServicio.ButtonSize = New System.Drawing.Size(53, 35)
        Me.tbServicio.DropDownArrows = True
        Me.tbServicio.ImageList = Me.ImageList1
        Me.tbServicio.Name = "tbServicio"
        Me.tbServicio.ShowToolTips = True
        Me.tbServicio.Size = New System.Drawing.Size(1002, 39)
        Me.tbServicio.TabIndex = 1
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 7
        Me.btnModificar.Text = "Modificar"
        '
        'btnPresupuesto
        '
        Me.btnPresupuesto.ImageIndex = 9
        Me.btnPresupuesto.Text = "Presupuesto"
        '
        'btnProgramacion
        '
        Me.btnProgramacion.ImageIndex = 2
        Me.btnProgramacion.Text = "Programación"
        '
        'btnHorario
        '
        Me.btnHorario.ImageIndex = 10
        Me.btnHorario.Text = "Horario"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 3
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList2
        '
        Me.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList2.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=Galgo;persist security info=False;user id=sa" & _
        ";workstation id=DESARROLLO_05;packet size=4096"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdHistorico})
        Me.GroupBox5.Location = New System.Drawing.Point(8, 472)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(480, 128)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Histórico de Servicios Tecnicos"
        '
        'grdHistorico
        '
        Me.grdHistorico.AlternatingBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdHistorico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdHistorico.CaptionText = "Equipo"
        Me.grdHistorico.DataMember = ""
        Me.grdHistorico.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdHistorico.Location = New System.Drawing.Point(8, 16)
        Me.grdHistorico.Name = "grdHistorico"
        Me.grdHistorico.ReadOnly = True
        Me.grdHistorico.SelectionForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdHistorico.Size = New System.Drawing.Size(464, 112)
        Me.grdHistorico.TabIndex = 0
        Me.grdHistorico.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSHistorico})
        '
        'DGTSHistorico
        '
        Me.DGTSHistorico.AlternatingBackColor = System.Drawing.Color.LightSteelBlue
        Me.DGTSHistorico.DataGrid = Me.grdHistorico
        Me.DGTSHistorico.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCPedidoReferencia, Me.DGTBCFCompromiso, Me.DGTBCFUsuario, Me.DGTBCStatus, Me.DGTBCTipoServicio, Me.DGTBCPuntos})
        Me.DGTSHistorico.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSHistorico.LinkColor = System.Drawing.SystemColors.InactiveCaption
        Me.DGTSHistorico.MappingName = "Historico"
        Me.DGTSHistorico.SelectionBackColor = System.Drawing.Color.DodgerBlue
        '
        'DGTBCPedidoReferencia
        '
        Me.DGTBCPedidoReferencia.Format = ""
        Me.DGTBCPedidoReferencia.FormatInfo = Nothing
        Me.DGTBCPedidoReferencia.HeaderText = "PedidoReferencia"
        Me.DGTBCPedidoReferencia.MappingName = "PedidoReferencia"
        Me.DGTBCPedidoReferencia.Width = 75
        '
        'DGTBCFCompromiso
        '
        Me.DGTBCFCompromiso.Format = ""
        Me.DGTBCFCompromiso.FormatInfo = Nothing
        Me.DGTBCFCompromiso.HeaderText = "FCompromiso"
        Me.DGTBCFCompromiso.MappingName = "FCompromiso"
        Me.DGTBCFCompromiso.Width = 75
        '
        'DGTBCFUsuario
        '
        Me.DGTBCFUsuario.Format = ""
        Me.DGTBCFUsuario.FormatInfo = Nothing
        Me.DGTBCFUsuario.HeaderText = "Usuario"
        Me.DGTBCFUsuario.MappingName = "Usuario"
        Me.DGTBCFUsuario.Width = 65
        '
        'DGTBCStatus
        '
        Me.DGTBCStatus.Format = ""
        Me.DGTBCStatus.FormatInfo = Nothing
        Me.DGTBCStatus.HeaderText = "Status"
        Me.DGTBCStatus.MappingName = "Status"
        Me.DGTBCStatus.Width = 65
        '
        'DGTBCTipoServicio
        '
        Me.DGTBCTipoServicio.Format = ""
        Me.DGTBCTipoServicio.FormatInfo = Nothing
        Me.DGTBCTipoServicio.HeaderText = "Tipo Servicio"
        Me.DGTBCTipoServicio.MappingName = "TipoServicio"
        Me.DGTBCTipoServicio.Width = 75
        '
        'DGTBCPuntos
        '
        Me.DGTBCPuntos.Format = ""
        Me.DGTBCPuntos.FormatInfo = Nothing
        Me.DGTBCPuntos.HeaderText = "Puntos"
        Me.DGTBCPuntos.MappingName = "Puntos"
        Me.DGTBCPuntos.Width = 45
        '
        'lblEtiqueta
        '
        Me.lblEtiqueta.Enabled = False
        Me.lblEtiqueta.Location = New System.Drawing.Point(144, 680)
        Me.lblEtiqueta.Name = "lblEtiqueta"
        Me.lblEtiqueta.Size = New System.Drawing.Size(88, 16)
        Me.lblEtiqueta.TabIndex = 258
        '
        'lblCamioneta
        '
        Me.lblCamioneta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCamioneta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCamioneta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCamioneta.Location = New System.Drawing.Point(584, 136)
        Me.lblCamioneta.Name = "lblCamioneta"
        Me.lblCamioneta.Size = New System.Drawing.Size(112, 21)
        Me.lblCamioneta.TabIndex = 259
        Me.lblCamioneta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHoraAtencion
        '
        Me.lblHoraAtencion.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblHoraAtencion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHoraAtencion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHoraAtencion.Location = New System.Drawing.Point(768, 12)
        Me.lblHoraAtencion.Name = "lblHoraAtencion"
        Me.lblHoraAtencion.Size = New System.Drawing.Size(160, 21)
        Me.lblHoraAtencion.TabIndex = 260
        Me.lblHoraAtencion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(656, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(110, 14)
        Me.Label16.TabIndex = 261
        Me.Label16.Text = "Horario de atención :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAñoPed
        '
        Me.lblAñoPed.Location = New System.Drawing.Point(608, 8)
        Me.lblAñoPed.Name = "lblAñoPed"
        Me.lblAñoPed.Size = New System.Drawing.Size(8, 23)
        Me.lblAñoPed.TabIndex = 262
        '
        'GBFormaPago
        '
        Me.GBFormaPago.Controls.AddRange(New System.Windows.Forms.Control() {Me.Button1, Me.Label28, Me.Label19, Me.Label31, Me.lblPagosde, Me.lblNumPagos, Me.lblDias, Me.Label18, Me.Label25, Me.Label27, Me.lstMaterial, Me.lblTotalEjecutivas, Me.btnEliminar, Me.btnAgregar, Me.Label21, Me.cboMaterial, Me.CboTipoCredito, Me.Label20, Me.txtDireccionInstalacion, Me.Label17})
        Me.GBFormaPago.Location = New System.Drawing.Point(8, 296)
        Me.GBFormaPago.Name = "GBFormaPago"
        Me.GBFormaPago.Size = New System.Drawing.Size(984, 168)
        Me.GBFormaPago.TabIndex = 0
        Me.GBFormaPago.TabStop = False
        Me.GBFormaPago.Text = "Forma de Pago"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(456, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 16)
        Me.Button1.TabIndex = 261
        Me.Button1.Text = "Button1"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(536, 56)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(154, 14)
        Me.Label28.TabIndex = 260
        Me.Label28.Text = "Lista de Material Seleccionado"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(408, 85)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(25, 14)
        Me.Label19.TabIndex = 259
        Me.Label19.Text = "Días"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(192, 80)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(24, 24)
        Me.Label31.TabIndex = 258
        Me.Label31.Text = "$"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPagosde
        '
        Me.lblPagosde.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPagosde.Location = New System.Drawing.Point(224, 80)
        Me.lblPagosde.Name = "lblPagosde"
        Me.lblPagosde.Size = New System.Drawing.Size(72, 24)
        Me.lblPagosde.TabIndex = 257
        Me.lblPagosde.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumPagos
        '
        Me.lblNumPagos.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumPagos.Location = New System.Drawing.Point(80, 80)
        Me.lblNumPagos.Name = "lblNumPagos"
        Me.lblNumPagos.Size = New System.Drawing.Size(56, 24)
        Me.lblNumPagos.TabIndex = 256
        Me.lblNumPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDias
        '
        Me.lblDias.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDias.Location = New System.Drawing.Point(352, 80)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(48, 24)
        Me.lblDias.TabIndex = 255
        Me.lblDias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(312, 85)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(33, 14)
        Me.Label18.TabIndex = 244
        Me.Label18.Text = "Cada:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(136, 85)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(54, 14)
        Me.Label25.TabIndex = 242
        Me.Label25.Text = "Pagos de:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(16, 85)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(65, 14)
        Me.Label27.TabIndex = 240
        Me.Label27.Text = "Núm Pagos:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lstMaterial
        '
        Me.lstMaterial.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lstMaterial.ColumnWidth = 2
        Me.lstMaterial.Location = New System.Drawing.Point(528, 80)
        Me.lstMaterial.Name = "lstMaterial"
        Me.lstMaterial.Size = New System.Drawing.Size(448, 82)
        Me.lstMaterial.TabIndex = 6
        '
        'lblTotalEjecutivas
        '
        Me.lblTotalEjecutivas.Location = New System.Drawing.Point(464, 48)
        Me.lblTotalEjecutivas.Name = "lblTotalEjecutivas"
        Me.lblTotalEjecutivas.Size = New System.Drawing.Size(40, 16)
        Me.lblTotalEjecutivas.TabIndex = 236
        '
        'btnEliminar
        '
        Me.btnEliminar.Location = New System.Drawing.Point(464, 120)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(48, 24)
        Me.btnEliminar.TabIndex = 5
        Me.btnEliminar.Text = "<<"
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(464, 88)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(48, 24)
        Me.btnAgregar.TabIndex = 4
        Me.btnAgregar.Text = ">>"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(16, 112)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(82, 14)
        Me.Label21.TabIndex = 231
        Me.Label21.Text = "Lista de Precios"
        '
        'cboMaterial
        '
        Me.cboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaterial.Location = New System.Drawing.Point(16, 136)
        Me.cboMaterial.Name = "cboMaterial"
        Me.cboMaterial.Size = New System.Drawing.Size(432, 21)
        Me.cboMaterial.TabIndex = 3
        '
        'CboTipoCredito
        '
        Me.CboTipoCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboTipoCredito.ItemHeight = 13
        Me.CboTipoCredito.Items.AddRange(New Object() {"Sin Crédito"})
        Me.CboTipoCredito.Location = New System.Drawing.Point(624, 24)
        Me.CboTipoCredito.Name = "CboTipoCredito"
        Me.CboTipoCredito.Size = New System.Drawing.Size(184, 21)
        Me.CboTipoCredito.TabIndex = 8
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(536, 27)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(85, 14)
        Me.Label20.TabIndex = 215
        Me.Label20.Text = "Tipo de Crédito:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDireccionInstalacion
        '
        Me.txtDireccionInstalacion.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtDireccionInstalacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDireccionInstalacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDireccionInstalacion.Location = New System.Drawing.Point(16, 40)
        Me.txtDireccionInstalacion.Multiline = True
        Me.txtDireccionInstalacion.Name = "txtDireccionInstalacion"
        Me.txtDireccionInstalacion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDireccionInstalacion.Size = New System.Drawing.Size(432, 32)
        Me.txtDireccionInstalacion.TabIndex = 2
        Me.txtDireccionInstalacion.Text = ""
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(16, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(103, 14)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Confirmar Dirección"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPuntos
        '
        Me.lblPuntos.Location = New System.Drawing.Point(632, 8)
        Me.lblPuntos.Name = "lblPuntos"
        Me.lblPuntos.Size = New System.Drawing.Size(8, 23)
        Me.lblPuntos.TabIndex = 264
        '
        'frmServicios1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1002, 611)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblPuntos, Me.GBFormaPago, Me.lblAñoPed, Me.Label16, Me.lblHoraAtencion, Me.lblCamioneta, Me.lblEtiqueta, Me.GroupBox5, Me.tbServicio, Me.GroupBox2, Me.GroupBox4})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "frmServicios1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Servicio"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.grdHistorico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBFormaPago.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub dtpFCompromiso_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If dtpFCompromiso.Value < Now.Date Or dtpFCompromiso.Value > Now.AddMonths(12) Then
            MsgBox("La fecha compromiso no puede ser menor a la dia de hoy o mayor a un año. Verifique.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            dtpFCompromiso.Select()
        End If

    End Sub




    Private Sub getData()
        Dim dataadapterCC As New SqlClient.SqlDataAdapter()

    End Sub



    Private Sub Servicios_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaCombos()
        Dim ActivaPuntos As Boolean
        lblStatusServicioTecnico.Visible = False
        lblPuntos.Visible = False
        lblTotalEjecutivas.Visible = False
        lblAñoPed.Visible = False
        lblAñoFolioPresupuesto.Enabled = False
        CargaDatosHistoricos()
        'LlenaCombos()
        'LLenaPrecio()
        lblFPedido.Text = Now.ToShortDateString

        tmHora.Enabled = True
        txtObservaciones.Select()
        DatosCargados = True
        btnPresupuesto.Enabled = False

        Dim Puntos As New SigaMetClasses.cConfig(11)
        ActivaPuntos = CType(Puntos.Parametros("ActivarLimitePuntos"), Boolean)
        If ActivaPuntos = True Then
            Do While SumaPuntos(dtpFCompromiso.Value.Date, CType(lblcelula.Text, Byte)) >= 20
                Dim Fecha As String
                Fecha = CType(dtpFCompromiso.Value, String)
                MessageBox.Show("Usted a llegado al limite de servicios técnicos del día " & Fecha & ", se le asignara una nueva fecha compromiso.", "Mensaje de Servicios Técnicos")
                dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                dtpFCompromiso.MinDate = dtpFCompromiso.Value
                If dtpFCompromiso.Value.DayOfWeek = DayOfWeek.Sunday Then
                    If dtpFCompromiso.Value.Date <> _
                                            SigaMetClasses.CalculaFechaCardinal(dtpFCompromiso.Value.AddDays(1).Year, CType(dtpFCompromiso.Value.AddDays(1).Month, SigaMetClasses.Enumeradores.enumMesAño), SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, SigaMetClasses.Enumeradores.enumCardinalidad.Segundo) Then
                        dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                        dtpFCompromiso.MinDate = dtpFCompromiso.Value
                    End If
                Else
                    'If dtpFCompromiso.Value.DayOfWeek = DayOfWeek.Saturday Then
                    '    If dtpFCompromiso.Value.Date <> _
                    '                           SigaMetClasses.CalculaFechaCardinal(dtpFCompromiso.Value.AddDays(1).Year, CType(dtpFCompromiso.Value.AddDays(1).Month, SigaMetClasses.Enumeradores.enumMesAño), SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, SigaMetClasses.Enumeradores.enumCardinalidad.Segundo) Then
                    '        dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                    '        dtpFCompromiso.MinDate = dtpFCompromiso.Value
                    '    End If
                    'End If
                    'If dtpFCompromiso.Value.DayOfWeek = DayOfWeek.Sunday Then
                    '    If dtpFCompromiso.Value.Date <> _
                    '                      SigaMetClasses.CalculaFechaCardinal(dtpFCompromiso.Value.AddDays(1).Year, CType(dtpFCompromiso.Value.AddDays(1).Month, SigaMetClasses.Enumeradores.enumMesAño), SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, SigaMetClasses.Enumeradores.enumCardinalidad.Segundo) Then
                    '        dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                    '        dtpFCompromiso.MinDate = dtpFCompromiso.Value
                    '    End If
                    'End If
                End If
            Loop


        End If

        If oSeguridad.TieneAcceso("MODIFICA FECHA") Then
            dtpFCompromiso.Value = dtpFCompromiso.Value
            dtpFCompromiso.MinDate = dtpFCompromiso.Value.AddMonths(-1)
        Else
            dtpFCompromiso.Value = _dtpFCompromiso.Value
            dtpFCompromiso.MinDate = dtpFCompromiso.Value
        End If

        txtDireccionInstalacion.Enabled = False

        CboTipoCredito.Enabled = False
        lblNumPagos.Enabled = False
        lblPagosde.Enabled = False
        lblDias.Enabled = False
        cboMaterial.Enabled = False
        btnAgregar.Enabled = False
        btnEliminar.Enabled = False
        lstMaterial.Enabled = False
        Label21.Enabled = False



        StatusPresupuesto()
    End Sub







    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()

    End Sub


    Private Sub ToolBar1_ButtonClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbServicio.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"



                'lblFcompromiso.Text = CType(dtpFCompromiso.Value, String)
                Dim strQuery As String = _
        "SELECT TotalPuntos,0 FROM vwSTSumaPuntos " & _
        "where producto = 4 and fcompromiso = '" & dtpFCompromiso.Value.ToShortDateString & "' and celula = " & lblcelula.Text

                Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
                Dim dtSumaPuntos As New DataTable("Suma")
                daSumaPuntos.Fill(dtSumaPuntos)
                If dtSumaPuntos.Rows.Count > 0 Then
                    lblPuntos.Text = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), String)
                Else
                    lblPuntos.Text = CType(0, String)
                End If
                'lblPuntos.Text = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), String)
                Dim TipoServicio As Integer = CType(cboTipoServicio.SelectedValue, Integer)
                If lblPuntos.Text >= CType(17, String) And lblPuntos.Text <= CType(19, String) Then

                    If TipoServicio = 28 Then
                        MessageBox.Show("Usted no puede ingresar un servicio de mas de 5 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                    Else
                        If TipoServicio = 30 Then
                            MessageBox.Show("Usted no puede ingresar un servicio de mas de 5 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                        Else
                            Try
                                SqlConnection.Close()
                                Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                'SqlConnection.ConnectionString = Conexion
                                SqlConnection.Open()
                            Catch dataException As Exception
                                MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
                            Finally
                                SqlConnection.Close()
                                'SqlConnection.Dispose()
                            End Try

                            Dim Transaccion As SqlClient.SqlTransaction
                            Dim cmdInsert As New SqlClient.SqlCommand()
                            Dim rdrInsert As SqlClient.SqlDataReader = Nothing
                            Dim SiGrabo As Boolean

                            cmdInsert.Connection = SqlConnection
                            cmdinsert.CommandTimeout = 300
                            SiGrabo = False
                            Transaccion = SqlConnection.BeginTransaction
                            cmdInsert.Transaction = Transaccion
                            Try

                                If _Pedido = 0 Then
                                    cmdInsert.CommandType = CommandType.StoredProcedure
                                    cmdInsert.CommandText = "spSTPedidoServiciotecnicoAlta"
                                    cmdInsert.Parameters.Clear()
                                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                                    cmdInsert.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                                    cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                                    cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                                    cmdInsert.Parameters.Add("@StatusServicioTecnico", SqlDbType.Char).Value = CType(lblStatus1.Text, String)
                                    cmdInsert.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                                    cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = CType(lblRuta.Text, Integer)
                                    cmdInsert.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                                    cmdinsert.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                                    cmdinsert.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = txtDireccionInstalacion.Text
                                    cmdinsert.Parameters.Add("@Material", SqlDbType.Int).Value = cboMaterial.SelectedValue
                                    cmdinsert.Parameters.Add("@TipoCredito", SqlDbType.Int).Value = CboTipoCredito.SelectedValue
                                    If lblNumPagos.Text = "" Then
                                        cmdinsert.Parameters.Add("@NumPagos", SqlDbType.Int).Value = 0
                                    Else
                                        cmdinsert.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                                    End If

                                    If ImporteLetra Is Nothing Then
                                        CMDinsert.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ""
                                    Else
                                        CMDinsert.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                                    End If

                                    CMDINSERT.Parameters.Add("@Puntos", SqlDbType.Int).Value = lblPuntos.Text
                                    If lblTotal.Text = "" Then
                                        cmdinsert.Parameters.Add("@Total", SqlDbType.Money).Value = 0
                                    Else
                                        cmdinsert.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                                    End If

                                    If Trim(lblHoraAtencion.Text) <> "" Then
                                        cmdInsert.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(lblHoraAtencion.Text, DateTime)
                                    End If



                                    cmdInsert.ExecuteNonQuery()

                                    ''Componente STClienteEquipo
                                    'Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                                    'Dim oClienteEquipo As New STClienteEquipo.cClienteEquipo()
                                    'For Each oEquipo In Me.ClienteEquipo.Equipos
                                    '    oClienteEquipo.Alta(CType(Me.lblContrato.Text, Integer), oEquipo.Equipo, 2, oEquipo.Precio)
                                    'Next
                                    ''Fin
                                Else

                                    cmdInsert.CommandType = CommandType.StoredProcedure
                                    cmdInsert.CommandText = "spSTModificaPedidoServicioTecnico"
                                    cmdInsert.Parameters.Clear()
                                    cmdInsert.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                                    cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                                    cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                                    cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                                    cmdInsert.Parameters.Add("@Anio", SqlDbType.Int).Value = _Anio
                                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                                    cmdInsert.ExecuteNonQuery()

                                End If

                                Transaccion.Commit()

                                SiGrabo = True

                            Catch er As Exception
                                Transaccion.Rollback()
                                MsgBox(er.Message, MsgBoxStyle.Critical)
                                SiGrabo = False
                            Finally
                                If SiGrabo Then
                                    Me.Close()
                                End If
                            End Try

                        End If

                    End If
                Else

                    Try
                        SqlConnection.Close()
                        'SqlConnection.ConnectionString = ConString
                        SqlConnection.Open()
                    Catch dataException As Exception
                        MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")

                    End Try

                    Dim Transaccion As SqlClient.SqlTransaction
                    Dim cmdInsert As New SqlClient.SqlCommand()
                    Dim rdrInsert As SqlClient.SqlDataReader = Nothing
                    Dim SiGrabo As Boolean

                    cmdInsert.Connection = SqlConnection
                    cmdinsert.CommandTimeout = 300
                    SiGrabo = False
                    Transaccion = SqlConnection.BeginTransaction
                    cmdInsert.Transaction = Transaccion
                    Try

                        If _Pedido = 0 Then
                            cmdInsert.CommandType = CommandType.StoredProcedure
                            cmdInsert.CommandText = "spSTPedidoServiciotecnicoAlta"
                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                            cmdInsert.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                            cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                            cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                            cmdInsert.Parameters.Add("@StatusServicioTecnico", SqlDbType.Char).Value = CType(lblStatus1.Text, String)
                            cmdInsert.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                            cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = CType(lblRuta.Text, Integer)
                            cmdInsert.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                            cmdinsert.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                            cmdinsert.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = txtDireccionInstalacion.Text
                            cmdinsert.Parameters.Add("@Puntos", SqlDbType.Int).Value = lblPuntos.Text
                            cmdinsert.Parameters.Add("@Material", SqlDbType.Int).Value = cboMaterial.SelectedValue
                            cmdinsert.Parameters.Add("@TipoCredito", SqlDbType.Int).Value = CType(CboTipoCredito.SelectedValue, Integer)
                            If lblNumPagos.Text = "" Then
                            Else
                                CMDinsert.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                            End If

                            If ImporteLetra Is Nothing Then
                            Else
                                CMDINSERT.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                            End If

                            If lblDias.Text = "" Then
                            Else
                                cmdinsert.Parameters.Add("@Dias", SqlDbType.Int).Value = lblDias.Text
                            End If

                            If lblPagosde.Text <> "" Then
                                cmdinsert.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = lblPagosde.Text
                            End If

                            If Trim(lblTotal.Text) <> "" Then
                                cmdinsert.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                            Else
                            End If

                            If Trim(lblHoraAtencion.Text) <> "" Then
                                cmdInsert.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(lblHoraAtencion.Text, DateTime)
                            End If



                            cmdInsert.ExecuteNonQuery()

                            ''Componente STClienteEquipo
                            'Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                            'Dim oClienteEquipo As New STClienteEquipo.cClienteEquipo()
                            'For Each oEquipo In Me.ClienteEquipo.Equipos
                            '    oClienteEquipo.Alta(CType(Me.lblContrato.Text, Integer), oEquipo.Equipo, 2, oEquipo.Precio)
                            'Next
                            ''Fin
                        Else

                            cmdInsert.CommandType = CommandType.StoredProcedure
                            cmdInsert.CommandText = "spSTModificaPedidoServicioTecnico"
                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                            cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                            cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                            cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                            cmdInsert.Parameters.Add("@Anio", SqlDbType.Int).Value = _Anio
                            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                            cmdInsert.ExecuteNonQuery()

                        End If

                        Transaccion.Commit()

                        SiGrabo = True

                    Catch er As Exception
                        Transaccion.Rollback()
                        MsgBox(er.Message, MsgBoxStyle.Critical)
                        SiGrabo = False
                    Finally
                        If SiGrabo Then
                            Me.Close()
                        End If
                    End Try



                End If



            Case "Modificar"



                Try
                    SqlConnection.Close()
                    'SqlConnection.ConnectionString = ConString
                    SqlConnection.Open()
                Catch dataException As Exception
                    MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
                End Try



                Dim Transaccion As SqlClient.SqlTransaction
                Dim cmdInsert As New SqlClient.SqlCommand()
                Dim rdrInsert As SqlClient.SqlDataReader = Nothing
                Dim SiGrabo As Boolean

                cmdInsert.Connection = SqlConnection
                cmdinsert.CommandTimeout = 300
                SiGrabo = False
                Transaccion = SqlConnection.BeginTransaction
                cmdInsert.Transaction = Transaccion
                Try


                    With cmdinsert
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "spSTModificaPedidoServiciosTecnicos"
                        .Parameters.Clear()
                        .Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                        .Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                        .Parameters.Add("@Tiposervicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                        .Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                        .Parameters.Add("@Pedido", SqlDbType.Int).Value = lblPedido.Text
                        .Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = Now.Year 'ESTO ES UNA CHARRADA
                        .Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(Me.lblHoraAtencion.Text, Date)

                        .ExecuteNonQuery()

                    End With


                    ''Componente STClienteEquipo
                    'Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                    'Dim oClienteEquipo As New STClienteEquipo.cClienteEquipo()
                    'For Each oEquipo In Me.ClienteEquipo.Equipos
                    '    oClienteEquipo.Alta(CType(Me.lblContrato.Text, Integer), oEquipo.Equipo, 2, oEquipo.Precio)
                    'Next
                    ''Fin


                    Transaccion.Commit()

                    SiGrabo = True

                Catch er As Exception
                    Transaccion.Rollback()
                    MsgBox(er.Message, MsgBoxStyle.Critical)
                    SiGrabo = False
                Finally
                    If SiGrabo Then
                        Me.Close()
                    End If
                End Try


            Case "Presupuesto"

                Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                ConexionTransaccion.Open()
                Dim SqlComando As New SqlCommand()
                Dim SqlTransaccion As SqlTransaction
                SqlComando.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                SqlComando.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                SqlComando.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                SqlComando.Parameters.Add("@Observaciones", SqlDbType.Char).Value = txtObservaciones.Text
                SqlComando.Parameters.Add("@Celula", SqlDbType.Char).Value = lblcelula.Text
                SqlComando.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                SqlComando.Parameters.Add("@StatusServicioTecnico", SqlDbType.Char).Value = lblStatus1.Text
                SqlComando.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                SqlComando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                SqlComando.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = txtDireccionInstalacion.Text
                SqlComando.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                SqlComando.Parameters.Add("@AñofolioPresupuesto", SqlDbType.Char).Value = lblAñoFolioPresupuesto.Text
                SqlComando.Parameters.Add("@FolioPresupuesto", SqlDbType.Int).Value = lblNumPresupuesto.Text
                SqlComando.Parameters.Add("@Material", SqlDbType.Int).Value = cboMaterial.SelectedValue
                SqlComando.Parameters.Add("@TipoCredito", SqlDbType.Int).Value = CboTipoCredito.SelectedValue
                If lblNumPagos.Text = "" Then
                    SqlComando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = 0
                Else
                    SqlComando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                End If

                If ImporteLetra Is Nothing Then
                    SqlComando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ""
                Else
                    SqlComando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                End If
                If lblDias.Text = "" Then
                    SqlComando.Parameters.Add("@dIAS", SqlDbType.Int).Value = 0
                Else
                    SqlComando.Parameters.Add("@dIAS", SqlDbType.Int).Value = lblDias.Text
                End If

                If lblPagosde.Text = "" Then
                    SqlComando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = 0
                Else
                    SqlComando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = CType(lblPagosde.Text, Integer)
                End If



                SqlTransaccion = ConexionTransaccion.BeginTransaction
                SqlComando.Connection = ConexionTransaccion
                SqlComando.Transaction = SqlTransaccion
                Try
                    If MsgBox("¿Quiere Aceptar el Presupuesto?", MsgBoxStyle.OKCancel) = MsgBoxResult.OK Then
                        SqlComando.CommandType = CommandType.StoredProcedure
                        SqlComando.CommandText = "spSTGeneraOrdenServicoTecnicoAutomatico"
                        SqlComando.CommandTimeout = 300
                        SqlComando.ExecuteNonQuery()
                        SqlTransaccion.Commit()
                    Else
                        btnPresupuesto.Enabled = False
                        btnAceptar.Enabled = True
                        Exit Try
                    End If
                Catch eException As Exception
                    SqlTransaccion.Rollback()
                    MsgBox(eException.Message)
                Finally
                    ConexionTransaccion.Close()
                    'ConexionTransaccion.Dispose()
                    Me.Close()
                End Try

            Case "Programación"

                If Now.DayOfWeek = DayOfWeek.Saturday Then
                    Dim programacion As New frmServProgramacion(_Usuario, _Clave, cnnSigamet.ConnectionString, 0, 0)
                    programacion.dtpFecha.Value = Now.Date.AddDays(2)
                    programacion.ShowDialog()
                Else
                    Dim programacion As New frmServProgramacion(_Usuario, _Clave, cnnSigamet.ConnectionString, 0, 0)
                    programacion.dtpFecha.Value = Now.Date.AddDays(1)
                    programacion.ShowDialog()
                End If
            Case "Horario"
                Cursor = Cursors.WaitCursor
                Dim hora As New frmhorario(dtpFCompromiso.Value, CType(lblcelula.Text, Byte))
                If hora.ShowDialog() = DialogResult.OK Then
                    Me.lblHoraAtencion.Text = hora.HoraSeleccionada.ToString
                End If
                Cursor = Cursors.Default

            Case "Cerrar"
                If MessageBox.Show("¿Desea salir de Servicio Técnicos?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub Servicios_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub


    Private Sub dbcboTipoPedido_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoPedido.SelectedIndexChanged
        If Presupuesto = "PRESUPUESTO" Then
            If DatosCargados Then
                TipoPedido = CType(Me.cboTipoPedido.SelectedValue, Integer)
                If TipoPedido = 7 Then
                    cboMaterial.Enabled = False
                    btnAgregar.Enabled = False
                    btnEliminar.Enabled = False
                    lstMaterial.Enabled = False
                    CboTipoCredito.Enabled = False
                    lblNumPagos.Enabled = False
                    lblPagosde.Enabled = False
                    lblDias.Enabled = False
                    Label21.Enabled = False
                    txtObservaciones.Text = ""
                    txtTrabajoRealizado.Text = ""
                Else
                    If TipoPedido = 10 Then
                        cboMaterial.Enabled = False
                        btnAgregar.Enabled = False
                        btnEliminar.Enabled = False
                        lstMaterial.Enabled = False
                        Label21.Enabled = False
                        CboTipoCredito.Enabled = True
                        CboTipoCredito.Visible = True
                        Label20.Visible = True
                        Label27.Visible = True
                        Label25.Visible = True
                        Label18.Visible = True
                        Label19.Visible = True
                        'lblNumPagos.Visible = True
                        lblPagosde.Visible = True
                        lblDias.Visible = True
                        lblNumPagos.Enabled = True
                        lblPagosde.Enabled = True
                        lblDias.Enabled = True
                        txtObservaciones.Text = ""
                        txtTrabajoRealizado.Text = ""
                    Else
                        If TipoPedido = 8 Then
                            txtDireccionInstalacion.Enabled = False
                            cboMaterial.Enabled = False
                            btnAgregar.Enabled = False
                            btnEliminar.Enabled = False
                            lstMaterial.Enabled = False
                            Label21.Enabled = False
                            CboTipoCredito.Visible = False
                            lblNumPagos.Visible = False
                            lblPagosde.Visible = False
                            lblDias.Visible = False
                            Label17.Visible = False
                            Label27.Visible = False
                            Label25.Visible = False
                            Label31.Visible = False
                            Label18.Visible = False
                            Label20.Visible = False
                            ClienteEquipo.Visible = False
                            txtObservaciones.Text = ""
                            txtTrabajoRealizado.Text = ""
                        Else
                            If TipoPedido = 9 Then
                                ClienteEquipo.Visible = True
                                txtDireccionInstalacion.Enabled = True
                            Else
                            End If
                        End If
                    End If
                End If
            Else
            End If
        Else
            If DatosCargados Then
                TipoPedido = CType(Me.cboTipoPedido.SelectedValue, Integer)
                If TipoPedido = 7 Then
                    txtDireccionInstalacion.Enabled = True
                    cboMaterial.Enabled = True
                    btnAgregar.Enabled = True
                    btnEliminar.Enabled = True
                    lstMaterial.Enabled = True
                    Label21.Enabled = True
                    CboTipoCredito.Visible = False
                    lblNumPagos.Visible = False
                    lblPagosde.Visible = False
                    lblDias.Visible = False
                    Label27.Visible = False
                    Label25.Visible = False
                    Label31.Visible = False
                    Label18.Visible = False
                    Label19.Visible = False
                    Label20.Visible = False
                    ClienteEquipo.Visible = False
                    txtObservaciones.Text = ""
                    txtTrabajoRealizado.Text = ""
                Else
                    If TipoPedido = 9 Then
                        ClienteEquipo.Visible = True
                        txtDireccionInstalacion.Enabled = True
                    Else
                        If TipoPedido = 10 Then
                            txtDireccionInstalacion.Enabled = True
                            cboMaterial.Enabled = True
                            btnAgregar.Enabled = True
                            btnEliminar.Enabled = True
                            lstMaterial.Enabled = True
                            Label21.Enabled = True
                            CboTipoCredito.Visible = True
                            CboTipoCredito.Enabled = True
                            lblNumPagos.Visible = True
                            lblNumPagos.Enabled = True
                            lblPagosde.Visible = True
                            lblPagosde.Enabled = True
                            lblDias.Visible = True
                            lblDias.Enabled = True
                            Label17.Visible = True
                            Label27.Visible = True
                            Label25.Visible = True
                            Label31.Visible = True
                            Label18.Visible = True
                            Label20.Visible = True
                            ClienteEquipo.Visible = False
                            txtObservaciones.Text = ""
                            txtTrabajoRealizado.Text = ""
                        Else
                            If TipoPedido = 8 Then
                                txtDireccionInstalacion.Enabled = False
                                cboMaterial.Enabled = False
                                btnAgregar.Enabled = False
                                btnEliminar.Enabled = False
                                lstMaterial.Enabled = False
                                Label21.Enabled = False
                                CboTipoCredito.Visible = False
                                lblNumPagos.Visible = False
                                lblPagosde.Visible = False
                                lblDias.Visible = False
                                Label17.Visible = False
                                Label27.Visible = False
                                Label25.Visible = False
                                Label31.Visible = False
                                Label18.Visible = False
                                Label20.Visible = False
                                ClienteEquipo.Visible = False
                                txtObservaciones.Text = ""
                                txtTrabajoRealizado.Text = ""
                            Else
                            End If
                        End If
                    End If
                End If

            Else

            End If
        End If


    End Sub

    Private Sub Servicios_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave

    End Sub



    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Me.Close()
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click

        Try
            SqlConnection.Close()
            'SqlConnection.ConnectionString = ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        Dim SiGrabo As Boolean

        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 300
        SiGrabo = False
        Transaccion = SqlConnection.BeginTransaction
        cmdInsert.Transaction = Transaccion

        Try
            If MsgBox("¿Quiere Aceptar el Presupuesto?", MsgBoxStyle.OKCancel) = MsgBoxResult.OK Then

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.CommandText = "update presupuestoserviciotecnico set status = 'ACEPTADO' WHERE foliopresupuesto=" & lblNumPresupuesto.Text
                cmdInsert.Parameters.Clear()
                'cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)

                cmdInsert.ExecuteNonQuery()

            Else

            End If

            Transaccion.Commit()

            SiGrabo = True

        Catch er As Exception
            Transaccion.Rollback()
            MsgBox(er.Message, MsgBoxStyle.Critical)
            SiGrabo = False
        Finally
            If SiGrabo Then
                Me.Close()
            End If
        End Try

    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        'abre la conexio de base de datos
        Try
            SqlConnection.Close()
            'SqlConnection.ConnectionString = ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try



        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        Dim SiGrabo As Boolean

        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 300
        SiGrabo = False
        Transaccion = SqlConnection.BeginTransaction
        cmdInsert.Transaction = Transaccion
        Try

            If _Tipo = 0 Then
                cmdInsert.CommandType = CommandType.StoredProcedure
                cmdInsert.CommandText = "spPedidoServiciotecnicoAlta"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                cmdInsert.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                cmdInsert.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                cmdInsert.Parameters.Add("@StatusServicioTecnico", SqlDbType.Char).Value = CType(lblStatus1.Text, String)
                cmdInsert.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = CType(lblRuta.Text, Integer)
                cmdInsert.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue


                cmdInsert.ExecuteNonQuery()

                'Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                'Dim oClienteEquipo As New STClienteEquipo.cClienteEquipo()
                'For Each oEquipo In Me.ClienteEquipo.Equipos
                '    oClienteEquipo.Alta(CType(Me.lblContrato.Text, Integer), oEquipo.Equipo, 2, oEquipo.Precio)
                'Next
            Else

                cmdInsert.CommandType = CommandType.StoredProcedure
                cmdInsert.CommandText = "sp_ModificaPedidoServicioTecnico"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(lblcelula.Text, Integer)
                cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                cmdInsert.Parameters.Add("@Anio", SqlDbType.Int).Value = _Anio
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                cmdInsert.ExecuteNonQuery()

            End If

            Transaccion.Commit()

            SiGrabo = True

        Catch er As Exception
            Transaccion.Rollback()
            MsgBox(er.Message, MsgBoxStyle.Critical)
            SiGrabo = False
        Finally
            If SiGrabo Then
                Me.Close()
            End If
        End Try
    End Sub


    Private Sub dbgrdHistorico_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdHistorico.CurrentCellChanged
        '_PedidoReferencia = CType(grdHistorico.Item(grdHistorico.CurrentRowIndex, 0), Integer)
        'lblAñoPed.Text = CType(grdHistorico.Item(grdHistorico.CurrentRowIndex, 1), String)

        lblStatusServicioTecnico.Text = CType(grdHistorico.Item(grdHistorico.CurrentRowIndex, 3), String)
        _PedidoReferencia = RTrim(CType(grdHistorico.Item(grdHistorico.CurrentRowIndex, 0), String))

        LlenaObservaciones()
        LlenaPresupuesto()
        LlenaTecnicos()
        'LlenaTipoPropiedad()

        'carga la etiqueta para la comparacion y de este modo desactivar los botones de servicio 
        Presupuesto = RTrim(CType(grdHistorico.Item(grdHistorico.CurrentRowIndex, 4), String))
        lstMaterial.Enabled = False
        If lblStatusPresupuesto.Text = "ACEPTADO" Then
            btnPresupuesto.Enabled = False
            btnAceptar.Enabled = True
            txtDireccionInstalacion.Enabled = False
            CboTipoCredito.Enabled = False
        Else

            If Presupuesto = "PRESUPUESTO" Then
                btnAceptar.Enabled = False
                btnPresupuesto.Enabled = True
                txtDireccionInstalacion.Enabled = True
                'CboTipoCredito.Enabled = False
                'lblNumPagos.Enabled = True
                'lblPagosde.Enabled = True
                'lblTotal.Enabled = True
                cboMaterial.Enabled = False
                btnAgregar.Enabled = False
                btnEliminar.Enabled = False
                lstMaterial.Enabled = False
            Else
                btnPresupuesto.Enabled = False
                btnAceptar.Enabled = False
            End If

        End If
        If RTrim(lblStatusServicioTecnico.Text) = "ATENDIDO" Then
            btnModificar.Enabled = False
            btnAceptar.Enabled = False
        Else
            btnModificar.Enabled = True
        End If
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click

    End Sub

    Private Sub lblAñoPed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAñoPed.Click

    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GBFormaPago.Enter

    End Sub



    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim Material As Integer
        Material = CType(cboMaterial.SelectedValue, Integer)
        Dim ConsMaterial As String = "select material,Descripcion + ' $ ' +cast(Precio as varchar (10))as descripcion,isnull(precio,0) AS Precio,UnidadMedida,Precio  from Material where material = " & Material
        Dim sqlMaterial As New SqlDataAdapter(ConsMaterial, cnnSigamet)
        Dim dtMaterial As New DataTable()
        sqlMaterial.Fill(dtMaterial)
        'lstMaterial.Items.Add(dtMaterial.Rows(0).Item("Descripcion"))

        Cuenta = CType(dtMaterial.Rows(0).Item("precio"), Integer)
        _Material = CType(dtMaterial.Rows(0).Item("material"), Integer)
        If Cuenta <> 0 Then
            lstMaterial.Items.Add(dtMaterial.Rows(0).Item("Descripcion"))
            _SubTotal = Cuenta
            'InsertaMaterial()
            If Suma > 0 Then
                Suma = CType(lblTotal.Text, Integer)
                Suma = Cuenta + Suma
                lblTotal.Text = CType(Suma, String)
            Else
                Suma = 0
                Suma = Cuenta + Suma
                lblTotal.Text = CType(Format(Suma, "###,###.##"), String)
            End If
        Else
            MessageBox.Show("El material no tiene precio, Pongase en contacto con célula 7", "Servicios Técnicos", MessageBoxButtons.OK)
        End If


    End Sub




    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        lstMaterial.Items.Remove(lstMaterial.SelectedItem)
        'txtPrecio.Text = " "
        'txtCantidad.Text = ""
        lblNumPagos.Text = ""
        lblDias.Text = ""
        lblPagosde.Text = ""
        lblTotal.Text = CType(0, String)

    End Sub


    Private Sub CboTipoCredito_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboTipoCredito.SelectedIndexChanged
        Dim TipoCredito As String
        TipoCredito = CType(CboTipoCredito.Text, String)
        If TipoCredito = "SIN CREDITO" Then
            lblNumPagos.Text = ""
            lblPagosde.Text = ""
            lblDias.Text = ""
        Else
            Dim Consulta As String
            Consulta = "select creditoserviciotecnico,descripcion, NumeroPagos,frecuencia from creditoserviciotecnico where descripcion = '" & TipoCredito & "'"
            Dim da As New SqlDataAdapter(Consulta, cnnSigamet)
            Dim dt As New DataTable("Pagare")
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                lblNumPagos.Text = CType(dt.Rows(0).Item("numeropagos"), String)
                lblDias.Text = CType(dt.Rows(0).Item("frecuencia"), String)
            End If


            If lblTotal.Text > "" Then
                'Dim a As New CantALetra.cCantALetraClass()

                Dim Importe As String = Nothing

                lblPagosde.Text = CType(Format(CType(lblTotal.Text, Double) / CType(lblNumPagos.Text, Double), "###,###.##"), String)
                'lblPagosde.Text = Format(lblPagosde.Text, "###,###.##")
                'a.Numero = CType((lblPagosde.Text), Double)
                'a.Unidad = "M.N."
                'a.Moneda = "Pesos"
                Dim a As New CantidadEnLetra.CantidadEnLetra(CDec(lblPagosde.Text))
                ImporteLetra = "**(" & a.CantidadEnLetras & ")**"
            End If
        End If

    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnAgregar.Enabled = True
        lstMaterial.Enabled = True
        btnEliminar.Enabled = True
    End Sub

    Private Sub cboMaterial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMaterial.SelectedIndexChanged
        btnAgregar.Enabled = True
        lstMaterial.Enabled = True
        btnEliminar.Enabled = True

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Cursor = Cursors.WaitCursor
        Dim Confirma As New frmConfirmaDireccion(CType(lblContrato.Text, Integer))
        Confirma.ShowDialog()
        Cursor = Cursors.Default
    End Sub
End Class
