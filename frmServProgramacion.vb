Imports System.Data.SqlClient
Public Class frmServProgramacion
    Inherits System.Windows.Forms.Form

    Public _UsuarioClave As String
    Public _Clave As String
    Private DatosCargados As Boolean = False
    Private _Pedido As String
    Private Pedido As Integer
    Private Celula As Integer
    Private AñoPed As Integer
    Private Client As String
    Private _Estatus As String
    Private Fcomp As Date
    Private _Folio As Integer
    Friend WithEvents btnConsultar As System.Windows.Forms.ToolBarButton



    Private _UsaLiquidacion As Boolean


    Private Sub Llenacelula()
        ''Dim LlenaCelula As New SqlDataAdapter("select celula,descripcion from celula where comercial = 1", cnnSigamet)
        Dim LlenaCelula As New SqlDataAdapter("exec spSEGUsuarioCelulaConsulta @Usuario", cnnSigamet)
        Dim dt As New DataTable("Celula")
        LlenaCelula.SelectCommand.Parameters.Add("@Usuario", SqlDbType.Char).Value = GLOBAL_Usuario

        LlenaCelula.Fill(dt)
        With cboCelula
            .DataSource = dt
            .DisplayMember = "descripcion"
            .ValueMember = "celula"
            .SelectedIndex = 0
        End With
        DatosCargados = True
    End Sub

    Private Sub SumServicios()
        lblTotalServicios.Text = CType(lvwProgramaciones.Items.Count(), String)
    End Sub

    Private Sub LlenaBitacora()
        Me.cboBitacora.DataSource = Nothing
        Dim UsuarioReprogramo As String

        Dim da As New SqlDataAdapter("select Pedido,Celula,TipoPedidoOriginal,FCompromisoOriginal,UsuarioOriginal,isnull(UsuarioReprogramo,'') as UsuarioReprogramo,FCambioReprogramo,FCompromisoActual,ObservacionesReprogramacion from  vwSTBitacoraReprogramacion  where pedido = " & Pedido & " and celula = " & Celula & " and añoped = " & AñoPed, cnnSigamet)
        Dim dt As DataTable
        dt = New DataTable("Bitacora")
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            UsuarioReprogramo = CType(dt.Rows(0).Item("UsuarioReprogramo"), String).Trim
            If UsuarioReprogramo Is "" Then
            Else
                Me.cboBitacora.DataSource = dt
            End If
        Else
        End If
    End Sub

    Private Sub LlenaLista()

        Dim sqlcomST As New SqlCommand("Select PedidoReferencia,Cliente,Rutadescripcion,FPedido,FCompromiso,Usuario,Status,TipoServicio,Puntos,TipoCobro,Tecnico,Programacion,Franquicia,Llamada" _
                                         & " From  vwSTLlenaProgranmacion Where tipocargo = 2 " _
                                          & "and fcompromiso >= ' " & dtpFecha.Value.ToShortDateString & " 00:00:00' " _
                                          & "and fcompromiso <= ' " & dtpFecha.Value.ToShortDateString & " 23:59:59' " _
                                          & "and celula = " & CType(cboCelula.SelectedValue, String), cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drProgST As SqlDataReader = sqlcomST.ExecuteReader


            'Borra todos los items
            Me.lvwProgramaciones.Items.Clear()

            While drProgST.Read
                Dim oProgST As ListViewItem = New ListViewItem(CType(drProgST("PedidoReferencia"), String), 12)



                If Not IsDBNull(drProgST("Status")) Then
                    If CType(drProgST("Status"), String).Trim = "ATENDIDO" Then oProgST.ImageIndex = 13
                    If CType(drProgST("Status"), String).Trim = "CANCELADO" Then oProgST.ImageIndex = 14
                    If CType(drProgST("Programacion"), Boolean) = True Then oProgST.ImageIndex = 2
                    If CType(drProgST("Franquicia"), Integer) = 1 Then oProgST.ImageIndex = 17
                    If CType(drProgST("Llamada"), Integer) = 1 Then oProgST.ImageIndex = 19
                    If CType(drProgST("Status"), String).Trim = "PENDIENTE" Then oProgST.ImageIndex = 20


                Else
                    oProgST.ImageIndex = 12
                End If


                oProgST.SubItems.Add(CType(drProgST("Cliente"), String))
                oProgST.SubItems.Add(RTrim(CType(drProgST("Rutadescripcion"), String)))
                oProgST.SubItems.Add(CType(drProgST("FPedido"), String))
                oProgST.SubItems.Add(CType(drProgST("FCompromiso"), String))
                oProgST.SubItems.Add(CType(drProgST("Usuario"), String))
                oProgST.SubItems.Add(CType(drProgST("status"), String))
                oProgST.SubItems.Add(CType(drProgST("TipoServicio"), String))
                oProgST.SubItems.Add(CType(drProgST("Puntos"), String))
                oProgST.SubItems.Add(CType(drProgST("TipoCobro"), String))
                oProgST.SubItems.Add(CType(drProgST("Tecnico"), String))

                lvwProgramaciones.Items.Add(oProgST)
                oProgST.EnsureVisible()
            End While

        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        'Permite agregar un control CheckBoxes a cada uno de los elemento de la lista
        'lvwProgramaciones.CheckBoxes = True


    End Sub

    Private Sub llenaEquipo()
        Dim SqlComando As New SqlCommand("select secuencia,equipo,tipopropiedad from vwstclienteequipo where (status = 'PENDIENTE' OR STATUS IS NULL) AND cliente = " & Client, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drEquipo As SqlDataReader = SqlComando.ExecuteReader
            Me.lvwEquipo.Items.Clear()
            While drEquipo.Read
                Dim oEqu As ListViewItem = New ListViewItem(CType(drEquipo("Secuencia"), String), 15)
                'oliq.SubItems.Add(CType(drLiq("PedidoReferencia"), String))
                oEqu.SubItems.Add(CType(drEquipo("Equipo"), String))
                oEqu.SubItems.Add(CType(drEquipo("TipoPropiedad"), String))
                lvwEquipo.Items.Add(oEqu)
                oEqu.EnsureVisible()
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub





    Private Sub SumPuntos()
        Dim Suma As New SqlCommand("Select ISNULL(TotalPuntos,0) AS TotalPuntos" _
                                         & " From  vwSTSumaPuntos Where fcompromiso >= ' " & dtpFecha.Value.ToShortDateString & " 00:00:00' " _
                                          & "and fcompromiso <= ' " & dtpFecha.Value.ToShortDateString & " 23:59:59' " _
                                          & "and celula = " & CType(cboCelula.SelectedValue, String), cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drSuma As SqlDataReader = Suma.ExecuteReader

            If drSuma.Read = True Then

                lblPuntos.Text = CType(drSuma("TotalPuntos"), String)

                'lblPuntos.Text = CType(0, String)
            Else
                'While drSuma.Read
                '    lblPuntos.Text = CType(drSuma("TotalPuntos"), String)
                'End While
                lblPuntos.Text = CType(0, String)
            End If

            'While drSuma.Read
            '    lblPuntos.Text = CType(drSuma("TotalPuntos"), String)
            'End While

            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub LlenaPedido()
        Dim Llena As New SqlCommand("select pedido,celula,AñoPed from Pedido where pedidoreferencia = '" & _Pedido & "' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drLlena As SqlDataReader = Llena.ExecuteReader
            While drLlena.Read
                Pedido = CType(drLlena("pedido"), Integer)
                Celula = CType(drLlena("celula"), Integer)
                AñoPed = CType(drLlena("añoped"), Integer)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub
    'Permite dar color a la listView
    Private Sub paintalternatingbackcolor(ByVal lv As ListView, ByVal color1 As Color, ByVal color2 As Color)
        Dim item As ListViewItem
        Dim SubItem As ListViewItem.ListViewSubItem
        For Each item In lv.Items
            If (item.Index Mod 2) = 0 Then
                item.BackColor = color1
            Else
                item.BackColor = color2
            End If
            For Each SubItem In item.SubItems
                SubItem.BackColor = item.BackColor
            Next
        Next
    End Sub

    Private Sub CargaDatosCliente()

        Try
            Dim da As New SqlDataAdapter("select Cliente,Celula,Ruta,Nombre,isnull(RazonSocial,'Sin empresa') as RazonSocial,Callenombre,isnull(NumInterior,'') as NumInterior,isnull(NumExterior,'') as NumExterior,isnull(ColoniaNombre,'')as ColoniaNombre,isnull(cp,'') as cp,Status,MunicipioNombre,isnull(TelCasa,'')as TelCasa,clasificacionclientedescripcion from vwdatoscliente Where Cliente = " & Client, cnnSigamet)
            Dim dt As New DataTable("Cliente")
            da.Fill(dt)
            'if apara la comparacion de que no haya dos mismos registros
            If dt.Rows.Count >= 1 Then
                lblCliente.Text = CType(dt.Rows(0).Item("Cliente"), String)
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
                lblClasificacionCliente.Text = CType(dt.Rows(0).Item("clasificacionclientedescripcion"), String)
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
    End Sub

    Private Sub LlenaObservaciones()
        Dim strQuery As String = "select isnull(ObservacionesServicioRealizado,'')as ObservacionesServicioRealizado from SERVICIOTECNICO where  pedido = " & Pedido & " and añoped = " & AñoPed & " And CELULA = " & Celula
        Dim daObservaciones As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtObservaciones As New DataTable("Observaciones")
        If dtObservaciones.Rows.Count > 0 Then
            daObservaciones.Fill(dtObservaciones)
            txtTrabajoRealizado.Text = CType(dtObservaciones.Rows(0).Item("observacionesserviciorealizado"), String)
        End If

    End Sub

    Private Sub LlenaServiciosTecnicos()
        Dim da As New SqlCommand("select PedidoReferencia,UsuarioCambio,Cliente,FAtencion,Pedido,AñoPed,Autotanque,chofer,ayudante,ObservacionesServicioRealizado,ObservacionesServicioTecnico,StatusServicioTecnico,isnull(fcompromisoinciial,'') as FCompromisoinciial " _
                                      & "from vwSTPestanaServicioTecnico Where pedido = '" & Pedido & "' And celula =  '" & Celula & "' and añoped = ' " & AñoPed & " ' ", cnnSigamet)
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
                lblPedidoReferencia.Text = CType(drST("Pedido"), String)
                lblFolio.Text = CType(drST("pedido"), String)
                lblUsuarioCambio.Text = CType(drST("UsuarioCambio"), String)

            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)

        End Try
    End Sub

    Private Sub LlenaPestañaPresupuesto()
        Dim strQuery As String = "select FolioPresupuesto,StatusPresupuesto,subtotal,ObservacionesPresupuesto,Descuento,Total from vwSTPestanaServicioTecnico where  pedido = " & Pedido & " And celula = " & Celula & " and añoped = " & AñoPed
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

    Private Sub OrdenAutomatica()
        Try
            Dim da As New SqlDataAdapter("select PedidoPrimero,TrabajoSolicitadoPrimero,TrabajoRealizadoPrimero,FolioPresupuestoViene,StatusPresupuestoViene,SubTotalPresupuesto,DescuentoPresupuesto,TotalPresupuesto,ObservacionesPresupuestoViene, PedidoAuto, NombreUsuario, TipoPedido, TipoServicio, TrabajoSolicitado from vwSTLlenaPedidoPresupuesto where pedidoauto = " & Pedido & "And celulaauto = " & Celula & "And añopedauto = " & AñoPed, cnnSigamet)
            Dim dt As New DataTable("Orden")
            da.Fill(dt)
            lblPedido.Text = CType(dt.Rows(0).Item("pedidoprimero"), String)
            txtTrabajoSolicitado.Text = CType(dt.Rows(0).Item("trabajosolicitadoprimero"), String)
            lblTrabajoRealizado.Text = CType(dt.Rows(0).Item("trabajorealizadoprimero"), String)
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

        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try

    End Sub

    Private Sub LlenaPagare()
        Try
            Dim daLlenaPagare As New SqlDataAdapter("select formapago,numeropagos,parcialidad,frecuencia from  vwSTLlenaFormaPagoCerrarOrden where pedido = " & Pedido & "and celula = " & Celula & "and añoped = " & AñoPed, cnnSigamet)
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

    End Sub

    Private Sub LlenaComodato()
        Dim daStatus As New SqlDataAdapter("SELECT Status FROM vwSTClienteEquipoPedido where pedido = " & Pedido & "and celula = " & Celula & "and añoped =" & AñoPed, cnnSigamet)
        Dim dtStatus As New DataTable("Comodato")
        daStatus.Fill(dtStatus)
        If dtStatus.Rows.Count <> 0 Then
            lblComodato.Text = CType(dtStatus.Rows(0).Item("Status"), String)
        Else
            lblComodato.Text = "Sin Comodato"
        End If
    End Sub


    Private Sub ExtraeDatosFranquicia()
        Dim da As New SqlDataAdapter("select * from vwSTExtraeInformacionFranquicia where finicioruta between '" & Now.Date & "'and'" & Now.Date & "'", cnnSigamet)
        Dim dt As New DataTable("Franquicias")
        da.Fill(dt)

    End Sub



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Usuario As String, ByVal Clave As String, ByVal CadenaConexion As String, ByVal Corporativo As Short, ByVal Sucursal As Short)
        MyBase.New()
        SigametSeguridad.Seguridad.Conexion = cnnSigamet

        GLOBAL_Usuario = Usuario
        GLOBAL_Password = Clave
        GLOBAL_CadenaConexion = CadenaConexion
        GLOBAL_Corporativo = Corporativo
        GLOBAL_Sucursal = Sucursal

        'oConfig2 = New SigaMetClasses.cConfig(11, GLOBAL_Corporativo, GLOBAL_Sucursal)


        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        oSeguridad = New SigaMetClasses.cSeguridad(Usuario, 11)

        GLOBAL_RutaReportes = CType(SigametSeguridad.Seguridad.Parametros(11, CType(GLOBAL_Corporativo, Byte), _
                        CType(GLOBAL_Sucursal, Byte)).ValorParametro("RutaReportesW7"), String)


        'Consulta UsuarioReportes, multiparámetros
        Dim oConfigS As New SigaMetClasses.cConfig(9, GLOBAL_Corporativo, GLOBAL_Sucursal)
        Dim oUsuarioReportes As New SigaMetClasses.cUserInfo()

        GLOBAL_UsuarioReporte = CType(oConfigS.Parametros("UsuarioReportes"), String)
        oUsuarioReportes.ConsultaDatosUsuarioReportes(GLOBAL_UsuarioReporte)
        GLOBAL_PasswordReporte = oUsuarioReportes.Password



        'Add any initialization after the InitializeComponent() call
        If oSeguridad.TieneAcceso("REPROGRAMAR") Then
            'btnModificar.Enabled = True

        End If
        If oSeguridad.TieneAcceso("CERRAR ORDEN") Then
            'btnCerrarOrden.Enabled = True
            Dim _FechaCorte As Date
            'Dim oConfig As New SigaMetClasses.cConfig(11)
            'ST_HoraCorte = CType(oConfig2.Parametros("horacorteentresemana"), String)
            ST_HoraCorte = CType(SigametSeguridad.Seguridad.Parametros(11, CType(GLOBAL_Corporativo, Byte), _
                        CType(GLOBAL_Sucursal, Byte)).ValorParametro("horacorteentresemana"), String)
            _FechaCorte = CType(Now.Date.ToShortDateString & " " & ST_HoraCorte, Date)
            If _FechaCorte < Now Then
                If oSeguridad.TieneAcceso("modifica fecha") Then
                    'btnLiquidacion.Enabled = True
                    ' btnCerrarOrden.Enabled = True
                Else
                    'btnLiquidacion.Enabled = False
                    btnAsignar.Enabled = False
                    btnCancelarOrden.Enabled = True
                    btnCancelarLiquidacion.Enabled = False
                    MenuItem1.Enabled = False
                    'Catalogos.Enabled = False
                End If
            Else
                'btnLiquidacion.Enabled = True
                'btnCerrarOrden.Enabled = True
                If oSeguridad.TieneAcceso("modifica fecha") Then
                    MenuItem1.Enabled = True
                    'Catalogos.Enabled = True
                Else
                    MenuItem34.Enabled = False
                    MenuItem44.Enabled = False
                    MenuItem47.Enabled = False
                    'Catalogos.Enabled = False
                    'btnLiquidacion.Enabled = False
                    btnAsignar.Enabled = False
                    btnReprogramar.Enabled = False
                    btnLiquidar.Enabled = False
                    btnCancelarLiquidacion.Enabled = False
                    btnCiclos.Enabled = False
                End If
            End If
        Else
            MenuItem34.Enabled = False
            MenuItem44.Enabled = False
            MenuItem47.Enabled = False
            'Catalogos.Enabled = False
            'btnLiquidacion.Enabled = False
            btnAsignar.Enabled = False
            btnReprogramar.Enabled = False
            btnLiquidar.Enabled = False
            btnCancelarLiquidacion.Enabled = False
            btnCiclos.Enabled = False
            btnPresupuesto.Enabled = False
            btnFranquicia.Enabled = False
            btnLlamada.Enabled = False
        End If
        If oSeguridad.TieneAcceso("BUSCAR") Then
            'btnBuscar.Enabled = True
            MenuItem1.Enabled = True
            'Catalogos.Enabled = True
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
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents tbtnReProgramar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnAsignar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnLiquidar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnCancancelarLiquidacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnCancelarOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnReporte As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnCiclos As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbtnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblTituloLista As System.Windows.Forms.Label
    Friend WithEvents lvwProgramacion As System.Windows.Forms.ListView
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem27 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem29 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem31 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem32 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem35 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem36 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem38 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem39 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem40 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem41 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem42 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem43 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem44 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem45 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem47 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem48 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolBar2 As System.Windows.Forms.ToolBar
    Friend WithEvents cboCelula As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnReprogramar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnAsignar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLiquidar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelarLiquidacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelarOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnReporteProgramacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCiclos As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lvwProgramaciones As System.Windows.Forms.ListView
    Friend WithEvents PedidoReferencia As System.Windows.Forms.ColumnHeader
    Friend WithEvents Cliente As System.Windows.Forms.ColumnHeader
    Friend WithEvents Ruta As System.Windows.Forms.ColumnHeader
    Friend WithEvents FPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents FCompromiso As System.Windows.Forms.ColumnHeader
    Friend WithEvents Usuario As System.Windows.Forms.ColumnHeader
    Friend WithEvents Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents TipoServicio As System.Windows.Forms.ColumnHeader
    Friend WithEvents Puntos As System.Windows.Forms.ColumnHeader
    Friend WithEvents TipoCobro As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tecnico As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPuntos As System.Windows.Forms.Label
    Friend WithEvents tbLlenaServicioTecnico As System.Windows.Forms.TabControl
    Friend WithEvents tpCliente As System.Windows.Forms.TabPage
    Friend WithEvents tpOrdenServicio As System.Windows.Forms.TabPage
    Friend WithEvents tpServicioTecnico As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtObservacionesPresupuesto As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblNPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblStatusPre As System.Windows.Forms.Label
    Friend WithEvents lblSubTotal As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDias As System.Windows.Forms.Label
    Friend WithEvents lblParcialidad As System.Windows.Forms.Label
    Friend WithEvents lblNumPagos As System.Windows.Forms.Label
    Friend WithEvents lblTipoPedido As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblHorario As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblContrato As System.Windows.Forms.Label
    Friend WithEvents lblFolio As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblAyudante As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents lblFAtencion As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents tpEquipoCliente As System.Windows.Forms.TabPage
    Friend WithEvents tpOrdenAutomatica As System.Windows.Forms.TabPage
    Friend WithEvents lblTotalServicios As System.Windows.Forms.Label
    Friend WithEvents MenuItem37 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem49 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem50 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem51 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem52 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem53 As System.Windows.Forms.MenuItem
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblClasificacionCliente As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblStatusCliente As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblTelefono As System.Windows.Forms.Label
    Friend WithEvents lblMunicipio As System.Windows.Forms.Label
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblColonia As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents lblNumeroInterior As System.Windows.Forms.Label
    Friend WithEvents lblNumeroExterior As System.Windows.Forms.Label
    Friend WithEvents lblCalle As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtObserv As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormaPago As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblTipoServicio As System.Windows.Forms.Label
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents txtTrabSolc As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents lblAutoPedido As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFolioPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblStatusPresupuesto As System.Windows.Forms.Label
    Friend WithEvents txtObservacionesPres As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents lblTot As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lblSubT As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoSolicitado As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents lvwEquipo As System.Windows.Forms.ListView
    Friend WithEvents ColSecuencia As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColEquipo As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColTipoPropiedad As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblPedidoReferencia As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblAñoPed As System.Windows.Forms.Label
    Friend WithEvents lblUsuarioCambio As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblComodato As System.Windows.Forms.Label
    Friend WithEvents btnPresupuesto As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFranquicia As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLlamada As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnPantallaFranquicia As System.Windows.Forms.ToolBarButton
    Friend WithEvents cboBitacora As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCelula As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoPedidoOriginal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCompromisoOriginal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCUsuarioOriginal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCUsuarioReprogramo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCambioReprogramo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCompromisoActual As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCObservacionesReprogramacion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnTodasFranquicias As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnObservacion As System.Windows.Forms.ToolBarButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServProgramacion))
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.MenuItem14 = New System.Windows.Forms.MenuItem()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.tbtnReProgramar = New System.Windows.Forms.ToolBarButton()
        Me.tbtnAsignar = New System.Windows.Forms.ToolBarButton()
        Me.tbtnLiquidar = New System.Windows.Forms.ToolBarButton()
        Me.tbtnCancancelarLiquidacion = New System.Windows.Forms.ToolBarButton()
        Me.tbtnCancelarOrden = New System.Windows.Forms.ToolBarButton()
        Me.tbtnReporte = New System.Windows.Forms.ToolBarButton()
        Me.tbtnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.tbtnCiclos = New System.Windows.Forms.ToolBarButton()
        Me.tbtnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.lblTituloLista = New System.Windows.Forms.Label()
        Me.lvwProgramacion = New System.Windows.Forms.ListView()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.MenuItem19 = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.MenuItem22 = New System.Windows.Forms.MenuItem()
        Me.MenuItem23 = New System.Windows.Forms.MenuItem()
        Me.MenuItem24 = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.MenuItem26 = New System.Windows.Forms.MenuItem()
        Me.MenuItem27 = New System.Windows.Forms.MenuItem()
        Me.MenuItem28 = New System.Windows.Forms.MenuItem()
        Me.MenuItem29 = New System.Windows.Forms.MenuItem()
        Me.MenuItem30 = New System.Windows.Forms.MenuItem()
        Me.MenuItem31 = New System.Windows.Forms.MenuItem()
        Me.MenuItem32 = New System.Windows.Forms.MenuItem()
        Me.MenuItem33 = New System.Windows.Forms.MenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem34 = New System.Windows.Forms.MenuItem()
        Me.MenuItem35 = New System.Windows.Forms.MenuItem()
        Me.MenuItem36 = New System.Windows.Forms.MenuItem()
        Me.MenuItem38 = New System.Windows.Forms.MenuItem()
        Me.MenuItem39 = New System.Windows.Forms.MenuItem()
        Me.MenuItem37 = New System.Windows.Forms.MenuItem()
        Me.MenuItem49 = New System.Windows.Forms.MenuItem()
        Me.MenuItem50 = New System.Windows.Forms.MenuItem()
        Me.MenuItem51 = New System.Windows.Forms.MenuItem()
        Me.MenuItem52 = New System.Windows.Forms.MenuItem()
        Me.MenuItem40 = New System.Windows.Forms.MenuItem()
        Me.MenuItem41 = New System.Windows.Forms.MenuItem()
        Me.MenuItem42 = New System.Windows.Forms.MenuItem()
        Me.MenuItem43 = New System.Windows.Forms.MenuItem()
        Me.MenuItem44 = New System.Windows.Forms.MenuItem()
        Me.MenuItem45 = New System.Windows.Forms.MenuItem()
        Me.MenuItem53 = New System.Windows.Forms.MenuItem()
        Me.MenuItem47 = New System.Windows.Forms.MenuItem()
        Me.MenuItem48 = New System.Windows.Forms.MenuItem()
        Me.ToolBar2 = New System.Windows.Forms.ToolBar()
        Me.btnReprogramar = New System.Windows.Forms.ToolBarButton()
        Me.btnConsultar = New System.Windows.Forms.ToolBarButton()
        Me.btnAsignar = New System.Windows.Forms.ToolBarButton()
        Me.btnObservacion = New System.Windows.Forms.ToolBarButton()
        Me.btnLiquidar = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarLiquidacion = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarOrden = New System.Windows.Forms.ToolBarButton()
        Me.btnPresupuesto = New System.Windows.Forms.ToolBarButton()
        Me.btnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.btnReporteProgramacion = New System.Windows.Forms.ToolBarButton()
        Me.btnCiclos = New System.Windows.Forms.ToolBarButton()
        Me.btnFranquicia = New System.Windows.Forms.ToolBarButton()
        Me.btnTodasFranquicias = New System.Windows.Forms.ToolBarButton()
        Me.btnPantallaFranquicia = New System.Windows.Forms.ToolBarButton()
        Me.btnLlamada = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.cboCelula = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lvwProgramaciones = New System.Windows.Forms.ListView()
        Me.PedidoReferencia = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Cliente = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Ruta = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FPedido = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FCompromiso = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Usuario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TipoServicio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Puntos = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TipoCobro = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Tecnico = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPuntos = New System.Windows.Forms.Label()
        Me.lblTotalServicios = New System.Windows.Forms.Label()
        Me.tbLlenaServicioTecnico = New System.Windows.Forms.TabControl()
        Me.tpCliente = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
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
        Me.lblClasificacionCliente = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
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
        Me.tpOrdenServicio = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtObserv = New System.Windows.Forms.TextBox()
        Me.tpServicioTecnico = New System.Windows.Forms.TabPage()
        Me.lblUsuarioCambio = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtObservacionesPresupuesto = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblNPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusPre = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblComodato = New System.Windows.Forms.Label()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.lblParcialidad = New System.Windows.Forms.Label()
        Me.lblNumPagos = New System.Windows.Forms.Label()
        Me.lblTipoPedido = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.lblFolio = New System.Windows.Forms.Label()
        Me.lblPedidoReferencia = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblAñoPed = New System.Windows.Forms.Label()
        Me.lblHorario = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblContrato = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
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
        Me.lvwEquipo = New System.Windows.Forms.ListView()
        Me.ColSecuencia = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColEquipo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColTipoPropiedad = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tpOrdenAutomatica = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.lblTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtTrabajoSolicitado = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.lblFolioPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusPresupuesto = New System.Windows.Forms.Label()
        Me.txtObservacionesPres = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblTot = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblSubT = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.lblFormaPago = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblTipoServicio = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.txtTrabSolc = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.lblAutoPedido = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboBitacora = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCelula = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoPedidoOriginal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCompromisoOriginal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCUsuarioOriginal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCUsuarioReprogramo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCambioReprogramo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCompromisoActual = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCObservacionesReprogramacion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tbLlenaServicioTecnico.SuspendLayout()
        Me.tpCliente.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.tpOrdenServicio.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.tpServicioTecnico.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tpEquipoCliente.SuspendLayout()
        Me.tpOrdenAutomatica.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.cboBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.Text = ""
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = -1
        Me.MenuItem2.Text = ""
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = -1
        Me.MenuItem3.Text = ""
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = -1
        Me.MenuItem4.Text = ""
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = -1
        Me.MenuItem5.Text = ""
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = -1
        Me.MenuItem6.Text = ""
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = -1
        Me.MenuItem7.Text = ""
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = -1
        Me.MenuItem8.Text = ""
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = -1
        Me.MenuItem9.Text = ""
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = -1
        Me.MenuItem10.Text = ""
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = -1
        Me.MenuItem11.Text = ""
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = -1
        Me.MenuItem12.Text = ""
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = -1
        Me.MenuItem13.Text = ""
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = -1
        Me.MenuItem14.Text = ""
        '
        'ToolBar1
        '
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(100, 42)
        Me.ToolBar1.TabIndex = 0
        '
        'tbtnReProgramar
        '
        Me.tbtnReProgramar.Name = "tbtnReProgramar"
        '
        'tbtnAsignar
        '
        Me.tbtnAsignar.Name = "tbtnAsignar"
        '
        'tbtnLiquidar
        '
        Me.tbtnLiquidar.Name = "tbtnLiquidar"
        '
        'tbtnCancancelarLiquidacion
        '
        Me.tbtnCancancelarLiquidacion.Name = "tbtnCancancelarLiquidacion"
        '
        'tbtnCancelarOrden
        '
        Me.tbtnCancelarOrden.Name = "tbtnCancelarOrden"
        '
        'tbtnReporte
        '
        Me.tbtnReporte.Name = "tbtnReporte"
        '
        'tbtnRefrescar
        '
        Me.tbtnRefrescar.Name = "tbtnRefrescar"
        '
        'tbtnCiclos
        '
        Me.tbtnCiclos.Name = "tbtnCiclos"
        '
        'tbtnCerrar
        '
        Me.tbtnCerrar.Name = "tbtnCerrar"
        '
        'lblTituloLista
        '
        Me.lblTituloLista.Location = New System.Drawing.Point(0, 0)
        Me.lblTituloLista.Name = "lblTituloLista"
        Me.lblTituloLista.Size = New System.Drawing.Size(100, 23)
        Me.lblTituloLista.TabIndex = 0
        '
        'lvwProgramacion
        '
        Me.lvwProgramacion.Location = New System.Drawing.Point(0, 0)
        Me.lvwProgramacion.Name = "lvwProgramacion"
        Me.lvwProgramacion.Size = New System.Drawing.Size(121, 97)
        Me.lvwProgramacion.TabIndex = 0
        Me.lvwProgramacion.UseCompatibleStateImageBehavior = False
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = -1
        Me.MenuItem15.Text = ""
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = -1
        Me.MenuItem16.Text = ""
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = -1
        Me.MenuItem17.Text = ""
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = -1
        Me.MenuItem18.Text = ""
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = -1
        Me.MenuItem19.Text = ""
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = -1
        Me.MenuItem20.Text = ""
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = -1
        Me.MenuItem21.Text = ""
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = -1
        Me.MenuItem22.Text = ""
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = -1
        Me.MenuItem23.Text = ""
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = -1
        Me.MenuItem24.Text = ""
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = -1
        Me.MenuItem25.Text = ""
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = -1
        Me.MenuItem26.Text = ""
        '
        'MenuItem27
        '
        Me.MenuItem27.Index = -1
        Me.MenuItem27.Text = ""
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = -1
        Me.MenuItem28.Text = ""
        '
        'MenuItem29
        '
        Me.MenuItem29.Index = -1
        Me.MenuItem29.Text = ""
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = -1
        Me.MenuItem30.Text = ""
        '
        'MenuItem31
        '
        Me.MenuItem31.Index = -1
        Me.MenuItem31.Text = ""
        '
        'MenuItem32
        '
        Me.MenuItem32.Index = -1
        Me.MenuItem32.Text = ""
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = -1
        Me.MenuItem33.Text = ""
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        Me.ImageList1.Images.SetKeyName(13, "")
        Me.ImageList1.Images.SetKeyName(14, "")
        Me.ImageList1.Images.SetKeyName(15, "")
        Me.ImageList1.Images.SetKeyName(16, "")
        Me.ImageList1.Images.SetKeyName(17, "")
        Me.ImageList1.Images.SetKeyName(18, "")
        Me.ImageList1.Images.SetKeyName(19, "")
        Me.ImageList1.Images.SetKeyName(20, "")
        Me.ImageList1.Images.SetKeyName(21, "")
        Me.ImageList1.Images.SetKeyName(22, "")
        Me.ImageList1.Images.SetKeyName(23, "")
        Me.ImageList1.Images.SetKeyName(24, "1441838438_view-content-window.ico")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem34, Me.MenuItem44, Me.MenuItem47})
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 0
        Me.MenuItem34.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem35, Me.MenuItem36, Me.MenuItem38, Me.MenuItem39, Me.MenuItem37, Me.MenuItem40, Me.MenuItem41, Me.MenuItem42, Me.MenuItem43})
        Me.MenuItem34.Text = "Programación"
        '
        'MenuItem35
        '
        Me.MenuItem35.Index = 0
        Me.MenuItem35.Text = "&Reprogramación"
        '
        'MenuItem36
        '
        Me.MenuItem36.Index = 1
        Me.MenuItem36.Text = "&Asignar"
        '
        'MenuItem38
        '
        Me.MenuItem38.Index = 2
        Me.MenuItem38.Text = "Cancelar Liq."
        '
        'MenuItem39
        '
        Me.MenuItem39.Index = 3
        Me.MenuItem39.Text = "Cancelar Ord."
        '
        'MenuItem37
        '
        Me.MenuItem37.Index = 4
        Me.MenuItem37.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem49, Me.MenuItem50, Me.MenuItem51, Me.MenuItem52})
        Me.MenuItem37.Text = "Imprimir"
        '
        'MenuItem49
        '
        Me.MenuItem49.Index = 0
        Me.MenuItem49.Text = "Orden de Servicio"
        '
        'MenuItem50
        '
        Me.MenuItem50.Index = 1
        Me.MenuItem50.Text = "Presupuestos"
        '
        'MenuItem51
        '
        Me.MenuItem51.Index = 2
        Me.MenuItem51.Text = "Pagares"
        '
        'MenuItem52
        '
        Me.MenuItem52.Index = 3
        Me.MenuItem52.Text = "Remisiones"
        '
        'MenuItem40
        '
        Me.MenuItem40.Index = 5
        Me.MenuItem40.Text = "Refrescar"
        '
        'MenuItem41
        '
        Me.MenuItem41.Index = 6
        Me.MenuItem41.Text = "Reporte Programaciones"
        '
        'MenuItem42
        '
        Me.MenuItem42.Index = 7
        Me.MenuItem42.Text = "Ciclos"
        '
        'MenuItem43
        '
        Me.MenuItem43.Index = 8
        Me.MenuItem43.Text = "Cerrar"
        '
        'MenuItem44
        '
        Me.MenuItem44.Index = 1
        Me.MenuItem44.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem45, Me.MenuItem53})
        Me.MenuItem44.Text = "Catálogos"
        '
        'MenuItem45
        '
        Me.MenuItem45.Index = 0
        Me.MenuItem45.Text = "Material"
        '
        'MenuItem53
        '
        Me.MenuItem53.Index = 1
        Me.MenuItem53.Text = "Equipo"
        '
        'MenuItem47
        '
        Me.MenuItem47.Index = 2
        Me.MenuItem47.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem48})
        Me.MenuItem47.Text = "Liquidación"
        '
        'MenuItem48
        '
        Me.MenuItem48.Index = 0
        Me.MenuItem48.Text = "Captura Liquidación"
        '
        'ToolBar2
        '
        Me.ToolBar2.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnReprogramar, Me.btnConsultar, Me.btnAsignar, Me.btnObservacion, Me.btnLiquidar, Me.btnCancelarLiquidacion, Me.btnCancelarOrden, Me.btnPresupuesto, Me.btnRefrescar, Me.btnReporteProgramacion, Me.btnCiclos, Me.btnFranquicia, Me.btnTodasFranquicias, Me.btnPantallaFranquicia, Me.btnLlamada, Me.btnCerrar})
        Me.ToolBar2.ButtonSize = New System.Drawing.Size(80, 36)
        Me.ToolBar2.Dock = System.Windows.Forms.DockStyle.Right
        Me.ToolBar2.DropDownArrows = True
        Me.ToolBar2.ImageList = Me.ImageList1
        Me.ToolBar2.Location = New System.Drawing.Point(946, 0)
        Me.ToolBar2.Name = "ToolBar2"
        Me.ToolBar2.ShowToolTips = True
        Me.ToolBar2.Size = New System.Drawing.Size(80, 583)
        Me.ToolBar2.TabIndex = 0
        Me.ToolBar2.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'btnReprogramar
        '
        Me.btnReprogramar.ImageIndex = 2
        Me.btnReprogramar.Name = "btnReprogramar"
        Me.btnReprogramar.Text = "Reprogramar"
        '
        'btnConsultar
        '
        Me.btnConsultar.ImageIndex = 24
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Text = "Consultar"
        '
        'btnAsignar
        '
        Me.btnAsignar.ImageIndex = 3
        Me.btnAsignar.Name = "btnAsignar"
        Me.btnAsignar.Text = "Asignar"
        '
        'btnObservacion
        '
        Me.btnObservacion.ImageIndex = 23
        Me.btnObservacion.Name = "btnObservacion"
        Me.btnObservacion.Text = "ST Observación"
        Me.btnObservacion.ToolTipText = "Agregar observación del Servicio Tecnico"
        '
        'btnLiquidar
        '
        Me.btnLiquidar.ImageIndex = 11
        Me.btnLiquidar.Name = "btnLiquidar"
        Me.btnLiquidar.Text = "Liquidar"
        '
        'btnCancelarLiquidacion
        '
        Me.btnCancelarLiquidacion.ImageIndex = 5
        Me.btnCancelarLiquidacion.Name = "btnCancelarLiquidacion"
        Me.btnCancelarLiquidacion.Text = "Cancel. Liq."
        '
        'btnCancelarOrden
        '
        Me.btnCancelarOrden.ImageIndex = 9
        Me.btnCancelarOrden.Name = "btnCancelarOrden"
        Me.btnCancelarOrden.Text = "Cancel. Ord."
        '
        'btnPresupuesto
        '
        Me.btnPresupuesto.ImageIndex = 16
        Me.btnPresupuesto.Name = "btnPresupuesto"
        Me.btnPresupuesto.Text = "Presupuesto"
        '
        'btnRefrescar
        '
        Me.btnRefrescar.ImageIndex = 7
        Me.btnRefrescar.Name = "btnRefrescar"
        Me.btnRefrescar.Text = "Refrescar"
        '
        'btnReporteProgramacion
        '
        Me.btnReporteProgramacion.ImageIndex = 10
        Me.btnReporteProgramacion.Name = "btnReporteProgramacion"
        Me.btnReporteProgramacion.Text = "Reporte Prog."
        '
        'btnCiclos
        '
        Me.btnCiclos.ImageIndex = 3
        Me.btnCiclos.Name = "btnCiclos"
        Me.btnCiclos.Text = "Ciclos"
        '
        'btnFranquicia
        '
        Me.btnFranquicia.ImageIndex = 17
        Me.btnFranquicia.Name = "btnFranquicia"
        Me.btnFranquicia.Text = "UnaFranquicia"
        Me.btnFranquicia.Visible = False
        '
        'btnTodasFranquicias
        '
        Me.btnTodasFranquicias.ImageIndex = 22
        Me.btnTodasFranquicias.Name = "btnTodasFranquicias"
        Me.btnTodasFranquicias.Text = "TFranquicias"
        Me.btnTodasFranquicias.Visible = False
        '
        'btnPantallaFranquicia
        '
        Me.btnPantallaFranquicia.ImageIndex = 21
        Me.btnPantallaFranquicia.Name = "btnPantallaFranquicia"
        Me.btnPantallaFranquicia.Text = "PantallaFranquicia"
        Me.btnPantallaFranquicia.Visible = False
        '
        'btnLlamada
        '
        Me.btnLlamada.ImageIndex = 18
        Me.btnLlamada.Name = "btnLlamada"
        Me.btnLlamada.Text = "Llamada"
        Me.btnLlamada.Visible = False
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 6
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Text = "Cerrar"
        '
        'cboCelula
        '
        Me.cboCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCelula.Location = New System.Drawing.Point(808, 8)
        Me.cboCelula.Name = "cboCelula"
        Me.cboCelula.Size = New System.Drawing.Size(104, 21)
        Me.cboCelula.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.YellowGreen
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(512, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 24)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "FProgramación:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(616, 8)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(88, 20)
        Me.dtpFecha.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.ForestGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(0, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(944, 32)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Ordenes de servicio incluidas en la programación de servicios técnicos:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwProgramaciones
        '
        Me.lvwProgramaciones.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvwProgramaciones.BackColor = System.Drawing.SystemColors.Window
        Me.lvwProgramaciones.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PedidoReferencia, Me.Cliente, Me.Ruta, Me.FPedido, Me.FCompromiso, Me.Usuario, Me.Status, Me.TipoServicio, Me.Puntos, Me.TipoCobro, Me.Tecnico})
        Me.lvwProgramaciones.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwProgramaciones.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lvwProgramaciones.FullRowSelect = True
        Me.lvwProgramaciones.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwProgramaciones.HoverSelection = True
        Me.lvwProgramaciones.LargeImageList = Me.ImageList1
        Me.lvwProgramaciones.Location = New System.Drawing.Point(0, 64)
        Me.lvwProgramaciones.Name = "lvwProgramaciones"
        Me.lvwProgramaciones.Size = New System.Drawing.Size(944, 192)
        Me.lvwProgramaciones.SmallImageList = Me.ImageList1
        Me.lvwProgramaciones.TabIndex = 282
        Me.lvwProgramaciones.UseCompatibleStateImageBehavior = False
        Me.lvwProgramaciones.View = System.Windows.Forms.View.Details
        '
        'PedidoReferencia
        '
        Me.PedidoReferencia.Text = "Pedido Referencia"
        Me.PedidoReferencia.Width = 100
        '
        'Cliente
        '
        Me.Cliente.Text = "Cliente"
        Me.Cliente.Width = 80
        '
        'Ruta
        '
        Me.Ruta.Text = "Ruta"
        Me.Ruta.Width = 70
        '
        'FPedido
        '
        Me.FPedido.Text = "Fecha Pedido"
        Me.FPedido.Width = 80
        '
        'FCompromiso
        '
        Me.FCompromiso.Text = "Fecha Compromiso"
        Me.FCompromiso.Width = 80
        '
        'Usuario
        '
        Me.Usuario.Text = "Usuario"
        Me.Usuario.Width = 65
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.Width = 75
        '
        'TipoServicio
        '
        Me.TipoServicio.Text = "Tipo Servicio"
        Me.TipoServicio.Width = 145
        '
        'Puntos
        '
        Me.Puntos.Text = "Puntos"
        Me.Puntos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Puntos.Width = 45
        '
        'TipoCobro
        '
        Me.TipoCobro.Text = "Tipo Cobro"
        Me.TipoCobro.Width = 95
        '
        'Tecnico
        '
        Me.Tecnico.Text = "Técnico"
        Me.Tecnico.Width = 185
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.ForestGreen
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 256)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(944, 32)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Datos de la orden de servicio técnico"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.ForestGreen
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(576, 320)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 16)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Total Puntos:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPuntos
        '
        Me.lblPuntos.BackColor = System.Drawing.Color.ForestGreen
        Me.lblPuntos.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPuntos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPuntos.Location = New System.Drawing.Point(704, 320)
        Me.lblPuntos.Name = "lblPuntos"
        Me.lblPuntos.Size = New System.Drawing.Size(40, 16)
        Me.lblPuntos.TabIndex = 23
        Me.lblPuntos.Text = "0"
        Me.lblPuntos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalServicios
        '
        Me.lblTotalServicios.BackColor = System.Drawing.Color.ForestGreen
        Me.lblTotalServicios.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalServicios.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTotalServicios.Location = New System.Drawing.Point(552, 40)
        Me.lblTotalServicios.Name = "lblTotalServicios"
        Me.lblTotalServicios.Size = New System.Drawing.Size(72, 16)
        Me.lblTotalServicios.TabIndex = 26
        Me.lblTotalServicios.Text = "0"
        Me.lblTotalServicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbLlenaServicioTecnico
        '
        Me.tbLlenaServicioTecnico.Controls.Add(Me.tpCliente)
        Me.tbLlenaServicioTecnico.Controls.Add(Me.tpOrdenServicio)
        Me.tbLlenaServicioTecnico.Controls.Add(Me.tpServicioTecnico)
        Me.tbLlenaServicioTecnico.Controls.Add(Me.tpEquipoCliente)
        Me.tbLlenaServicioTecnico.Controls.Add(Me.tpOrdenAutomatica)
        Me.tbLlenaServicioTecnico.Location = New System.Drawing.Point(0, 376)
        Me.tbLlenaServicioTecnico.Name = "tbLlenaServicioTecnico"
        Me.tbLlenaServicioTecnico.SelectedIndex = 0
        Me.tbLlenaServicioTecnico.Size = New System.Drawing.Size(944, 208)
        Me.tbLlenaServicioTecnico.TabIndex = 27
        '
        'tpCliente
        '
        Me.tpCliente.BackColor = System.Drawing.Color.YellowGreen
        Me.tpCliente.Controls.Add(Me.GroupBox3)
        Me.tpCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpCliente.Location = New System.Drawing.Point(4, 22)
        Me.tpCliente.Name = "tpCliente"
        Me.tpCliente.Size = New System.Drawing.Size(936, 182)
        Me.tpCliente.TabIndex = 1
        Me.tpCliente.Text = "Cliente"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblNumeroInterior)
        Me.GroupBox3.Controls.Add(Me.lblNumeroExterior)
        Me.GroupBox3.Controls.Add(Me.lblCalle)
        Me.GroupBox3.Controls.Add(Me.Label64)
        Me.GroupBox3.Controls.Add(Me.Label65)
        Me.GroupBox3.Controls.Add(Me.Label66)
        Me.GroupBox3.Controls.Add(Me.lblCliente)
        Me.GroupBox3.Controls.Add(Me.lblCelula)
        Me.GroupBox3.Controls.Add(Me.lblRuta)
        Me.GroupBox3.Controls.Add(Me.lblEmpresa)
        Me.GroupBox3.Controls.Add(Me.lblNombre)
        Me.GroupBox3.Controls.Add(Me.Label67)
        Me.GroupBox3.Controls.Add(Me.Label68)
        Me.GroupBox3.Controls.Add(Me.Label69)
        Me.GroupBox3.Controls.Add(Me.Label70)
        Me.GroupBox3.Controls.Add(Me.Label71)
        Me.GroupBox3.Controls.Add(Me.lblClasificacionCliente)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.lblStatusCliente)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.lblTelefono)
        Me.GroupBox3.Controls.Add(Me.lblMunicipio)
        Me.GroupBox3.Controls.Add(Me.lblCP)
        Me.GroupBox3.Controls.Add(Me.lblColonia)
        Me.GroupBox3.Controls.Add(Me.Label59)
        Me.GroupBox3.Controls.Add(Me.Label60)
        Me.GroupBox3.Controls.Add(Me.Label61)
        Me.GroupBox3.Controls.Add(Me.Label62)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1000, 168)
        Me.GroupBox3.TabIndex = 235
        Me.GroupBox3.TabStop = False
        '
        'lblNumeroInterior
        '
        Me.lblNumeroInterior.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumeroInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroInterior.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblNumeroInterior.Location = New System.Drawing.Point(280, 138)
        Me.lblNumeroInterior.Name = "lblNumeroInterior"
        Me.lblNumeroInterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroInterior.TabIndex = 274
        '
        'lblNumeroExterior
        '
        Me.lblNumeroExterior.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumeroExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroExterior.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblNumeroExterior.Location = New System.Drawing.Point(88, 138)
        Me.lblNumeroExterior.Name = "lblNumeroExterior"
        Me.lblNumeroExterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroExterior.TabIndex = 273
        '
        'lblCalle
        '
        Me.lblCalle.BackColor = System.Drawing.SystemColors.Control
        Me.lblCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblCalle.Location = New System.Drawing.Point(88, 106)
        Me.lblCalle.Name = "lblCalle"
        Me.lblCalle.Size = New System.Drawing.Size(304, 21)
        Me.lblCalle.TabIndex = 272
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label64.Location = New System.Drawing.Point(208, 141)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(55, 13)
        Me.Label64.TabIndex = 271
        Me.Label64.Text = "#Interior:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label65.Location = New System.Drawing.Point(24, 141)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(57, 13)
        Me.Label65.TabIndex = 270
        Me.Label65.Text = "#Exterior:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label66.Location = New System.Drawing.Point(24, 109)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(34, 13)
        Me.Label66.TabIndex = 269
        Me.Label66.Text = "Calle:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblCliente.Location = New System.Drawing.Point(88, 10)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(88, 21)
        Me.lblCliente.TabIndex = 268
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.Control
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblCelula.Location = New System.Drawing.Point(240, 10)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(48, 21)
        Me.lblCelula.TabIndex = 267
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.Control
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblRuta.Location = New System.Drawing.Point(344, 10)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(48, 21)
        Me.lblRuta.TabIndex = 266
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpresa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblEmpresa.Location = New System.Drawing.Point(88, 74)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(304, 21)
        Me.lblEmpresa.TabIndex = 265
        '
        'lblNombre
        '
        Me.lblNombre.BackColor = System.Drawing.SystemColors.Control
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblNombre.Location = New System.Drawing.Point(88, 42)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(304, 21)
        Me.lblNombre.TabIndex = 264
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label67.Location = New System.Drawing.Point(24, 77)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(52, 13)
        Me.Label67.TabIndex = 263
        Me.Label67.Text = "Empresa:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label68.Location = New System.Drawing.Point(24, 45)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(48, 13)
        Me.Label68.TabIndex = 262
        Me.Label68.Text = "Nombre:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label69.Location = New System.Drawing.Point(192, 13)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(40, 13)
        Me.Label69.TabIndex = 261
        Me.Label69.Text = "Celula:"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label70.Location = New System.Drawing.Point(296, 13)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(34, 13)
        Me.Label70.TabIndex = 260
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label71.Location = New System.Drawing.Point(24, 13)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(54, 13)
        Me.Label71.TabIndex = 259
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblClasificacionCliente
        '
        Me.lblClasificacionCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblClasificacionCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClasificacionCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClasificacionCliente.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblClasificacionCliente.Location = New System.Drawing.Point(544, 122)
        Me.lblClasificacionCliente.Name = "lblClasificacionCliente"
        Me.lblClasificacionCliente.Size = New System.Drawing.Size(368, 21)
        Me.lblClasificacionCliente.TabIndex = 258
        Me.lblClasificacionCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(432, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 13)
        Me.Label6.TabIndex = 257
        Me.Label6.Text = "Clasificación Cliente :"
        '
        'lblStatusCliente
        '
        Me.lblStatusCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusCliente.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblStatusCliente.Location = New System.Drawing.Point(712, 90)
        Me.lblStatusCliente.Name = "lblStatusCliente"
        Me.lblStatusCliente.Size = New System.Drawing.Size(200, 21)
        Me.lblStatusCliente.TabIndex = 256
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label14.Location = New System.Drawing.Point(656, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(42, 13)
        Me.Label14.TabIndex = 255
        Me.Label14.Text = "Status:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTelefono
        '
        Me.lblTelefono.BackColor = System.Drawing.SystemColors.Control
        Me.lblTelefono.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTelefono.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelefono.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblTelefono.Location = New System.Drawing.Point(544, 90)
        Me.lblTelefono.Name = "lblTelefono"
        Me.lblTelefono.Size = New System.Drawing.Size(104, 21)
        Me.lblTelefono.TabIndex = 254
        '
        'lblMunicipio
        '
        Me.lblMunicipio.BackColor = System.Drawing.SystemColors.Control
        Me.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMunicipio.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblMunicipio.Location = New System.Drawing.Point(712, 58)
        Me.lblMunicipio.Name = "lblMunicipio"
        Me.lblMunicipio.Size = New System.Drawing.Size(200, 21)
        Me.lblMunicipio.TabIndex = 253
        '
        'lblCP
        '
        Me.lblCP.BackColor = System.Drawing.SystemColors.Control
        Me.lblCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCP.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblCP.Location = New System.Drawing.Point(544, 58)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(104, 21)
        Me.lblCP.TabIndex = 252
        '
        'lblColonia
        '
        Me.lblColonia.BackColor = System.Drawing.SystemColors.Control
        Me.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColonia.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblColonia.Location = New System.Drawing.Point(544, 26)
        Me.lblColonia.Name = "lblColonia"
        Me.lblColonia.Size = New System.Drawing.Size(368, 21)
        Me.lblColonia.TabIndex = 251
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label59.Location = New System.Drawing.Point(656, 62)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(54, 13)
        Me.Label59.TabIndex = 250
        Me.Label59.Text = "Municipio:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label60.Location = New System.Drawing.Point(432, 93)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(53, 13)
        Me.Label60.TabIndex = 249
        Me.Label60.Text = "Telefono:"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label61.Location = New System.Drawing.Point(432, 61)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(24, 13)
        Me.Label61.TabIndex = 248
        Me.Label61.Text = "CP:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label62.Location = New System.Drawing.Point(432, 29)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(46, 13)
        Me.Label62.TabIndex = 247
        Me.Label62.Text = "Colonia:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tpOrdenServicio
        '
        Me.tpOrdenServicio.BackColor = System.Drawing.SystemColors.Control
        Me.tpOrdenServicio.Controls.Add(Me.GroupBox5)
        Me.tpOrdenServicio.Controls.Add(Me.GroupBox4)
        Me.tpOrdenServicio.Location = New System.Drawing.Point(4, 22)
        Me.tpOrdenServicio.Name = "tpOrdenServicio"
        Me.tpOrdenServicio.Size = New System.Drawing.Size(936, 182)
        Me.tpOrdenServicio.TabIndex = 3
        Me.tpOrdenServicio.Text = "Observaciones"
        Me.tpOrdenServicio.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtTrabajoRealizado)
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox5.Location = New System.Drawing.Point(488, 8)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(528, 168)
        Me.GroupBox5.TabIndex = 224
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Trabajo Realizado"
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.Control
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(8, 16)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ReadOnly = True
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(432, 144)
        Me.txtTrabajoRealizado.TabIndex = 223
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.YellowGreen
        Me.GroupBox4.Controls.Add(Me.txtObserv)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox4.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(472, 168)
        Me.GroupBox4.TabIndex = 223
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Trabajo Solicitado"
        '
        'txtObserv
        '
        Me.txtObserv.BackColor = System.Drawing.SystemColors.Control
        Me.txtObserv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObserv.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObserv.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtObserv.Location = New System.Drawing.Point(12, 16)
        Me.txtObserv.Multiline = True
        Me.txtObserv.Name = "txtObserv"
        Me.txtObserv.ReadOnly = True
        Me.txtObserv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObserv.Size = New System.Drawing.Size(452, 144)
        Me.txtObserv.TabIndex = 221
        '
        'tpServicioTecnico
        '
        Me.tpServicioTecnico.BackColor = System.Drawing.Color.YellowGreen
        Me.tpServicioTecnico.Controls.Add(Me.lblUsuarioCambio)
        Me.tpServicioTecnico.Controls.Add(Me.Label2)
        Me.tpServicioTecnico.Controls.Add(Me.GroupBox2)
        Me.tpServicioTecnico.Controls.Add(Me.GroupBox1)
        Me.tpServicioTecnico.Controls.Add(Me.lblPedidoReferencia)
        Me.tpServicioTecnico.Controls.Add(Me.Label20)
        Me.tpServicioTecnico.Controls.Add(Me.lblAñoPed)
        Me.tpServicioTecnico.Controls.Add(Me.lblHorario)
        Me.tpServicioTecnico.Controls.Add(Me.Label18)
        Me.tpServicioTecnico.Controls.Add(Me.Label12)
        Me.tpServicioTecnico.Controls.Add(Me.lblContrato)
        Me.tpServicioTecnico.Controls.Add(Me.lblStatus)
        Me.tpServicioTecnico.Controls.Add(Me.Label15)
        Me.tpServicioTecnico.Controls.Add(Me.Label16)
        Me.tpServicioTecnico.Controls.Add(Me.Label19)
        Me.tpServicioTecnico.Controls.Add(Me.Label21)
        Me.tpServicioTecnico.Controls.Add(Me.Label22)
        Me.tpServicioTecnico.Controls.Add(Me.Label30)
        Me.tpServicioTecnico.Controls.Add(Me.lblAyudante)
        Me.tpServicioTecnico.Controls.Add(Me.lblTecnico)
        Me.tpServicioTecnico.Controls.Add(Me.Label42)
        Me.tpServicioTecnico.Controls.Add(Me.Label39)
        Me.tpServicioTecnico.Controls.Add(Me.lblUnidad)
        Me.tpServicioTecnico.Controls.Add(Me.lblFAtencion)
        Me.tpServicioTecnico.Controls.Add(Me.Label41)
        Me.tpServicioTecnico.Controls.Add(Me.Label40)
        Me.tpServicioTecnico.Controls.Add(Me.Label28)
        Me.tpServicioTecnico.Controls.Add(Me.Label26)
        Me.tpServicioTecnico.Location = New System.Drawing.Point(4, 22)
        Me.tpServicioTecnico.Name = "tpServicioTecnico"
        Me.tpServicioTecnico.Size = New System.Drawing.Size(936, 182)
        Me.tpServicioTecnico.TabIndex = 2
        Me.tpServicioTecnico.Text = "Servicio Técnico"
        Me.tpServicioTecnico.Visible = False
        '
        'lblUsuarioCambio
        '
        Me.lblUsuarioCambio.BackColor = System.Drawing.SystemColors.Control
        Me.lblUsuarioCambio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuarioCambio.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblUsuarioCambio.Location = New System.Drawing.Point(80, 157)
        Me.lblUsuarioCambio.Name = "lblUsuarioCambio"
        Me.lblUsuarioCambio.Size = New System.Drawing.Size(304, 21)
        Me.lblUsuarioCambio.TabIndex = 336
        Me.lblUsuarioCambio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(8, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 335
        Me.Label2.Text = "Reprogramo:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtObservacionesPresupuesto)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.lblTotal)
        Me.GroupBox2.Controls.Add(Me.lblDescuento)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.lblNPresupuesto)
        Me.GroupBox2.Controls.Add(Me.lblStatusPre)
        Me.GroupBox2.Controls.Add(Me.lblSubTotal)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox2.Location = New System.Drawing.Point(624, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 168)
        Me.GroupBox2.TabIndex = 334
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Presupuesto"
        '
        'txtObservacionesPresupuesto
        '
        Me.txtObservacionesPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.txtObservacionesPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesPresupuesto.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtObservacionesPresupuesto.Location = New System.Drawing.Point(168, 40)
        Me.txtObservacionesPresupuesto.Multiline = True
        Me.txtObservacionesPresupuesto.Name = "txtObservacionesPresupuesto"
        Me.txtObservacionesPresupuesto.ReadOnly = True
        Me.txtObservacionesPresupuesto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesPresupuesto.Size = New System.Drawing.Size(136, 120)
        Me.txtObservacionesPresupuesto.TabIndex = 334
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(168, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(141, 13)
        Me.Label17.TabIndex = 333
        Me.Label17.Text = "Observaciones Presupuesto"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 332
        Me.Label7.Text = "Descuento:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 139)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 13)
        Me.Label10.TabIndex = 331
        Me.Label10.Text = "Total:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(80, 136)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(80, 21)
        Me.lblTotal.TabIndex = 330
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDescuento
        '
        Me.lblDescuento.BackColor = System.Drawing.SystemColors.Control
        Me.lblDescuento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescuento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescuento.Location = New System.Drawing.Point(80, 112)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(80, 21)
        Me.lblDescuento.TabIndex = 329
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 328
        Me.Label9.Text = "Status:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 13)
        Me.Label11.TabIndex = 327
        Me.Label11.Text = " Folio:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNPresupuesto
        '
        Me.lblNPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblNPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPresupuesto.Location = New System.Drawing.Point(80, 40)
        Me.lblNPresupuesto.Name = "lblNPresupuesto"
        Me.lblNPresupuesto.Size = New System.Drawing.Size(80, 21)
        Me.lblNPresupuesto.TabIndex = 326
        Me.lblNPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusPre
        '
        Me.lblStatusPre.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusPre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusPre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusPre.Location = New System.Drawing.Point(80, 64)
        Me.lblStatusPre.Name = "lblStatusPre"
        Me.lblStatusPre.Size = New System.Drawing.Size(80, 21)
        Me.lblStatusPre.TabIndex = 325
        Me.lblStatusPre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSubTotal
        '
        Me.lblSubTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTotal.Location = New System.Drawing.Point(80, 88)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.Size = New System.Drawing.Size(80, 21)
        Me.lblSubTotal.TabIndex = 324
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 91)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 13)
        Me.Label13.TabIndex = 323
        Me.Label13.Text = "SubTotal:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.lblComodato)
        Me.GroupBox1.Controls.Add(Me.lblDias)
        Me.GroupBox1.Controls.Add(Me.lblParcialidad)
        Me.GroupBox1.Controls.Add(Me.lblNumPagos)
        Me.GroupBox1.Controls.Add(Me.lblTipoPedido)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label43)
        Me.GroupBox1.Controls.Add(Me.lblFolio)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(392, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 168)
        Me.GroupBox1.TabIndex = 333
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pagaré"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(8, 51)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(63, 13)
        Me.Label23.TabIndex = 347
        Me.Label23.Text = " Comodato:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComodato
        '
        Me.lblComodato.BackColor = System.Drawing.SystemColors.Control
        Me.lblComodato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblComodato.Location = New System.Drawing.Point(88, 48)
        Me.lblComodato.Name = "lblComodato"
        Me.lblComodato.Size = New System.Drawing.Size(124, 21)
        Me.lblComodato.TabIndex = 346
        Me.lblComodato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDias
        '
        Me.lblDias.BackColor = System.Drawing.SystemColors.Control
        Me.lblDias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDias.Location = New System.Drawing.Point(88, 144)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(64, 21)
        Me.lblDias.TabIndex = 345
        Me.lblDias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblParcialidad
        '
        Me.lblParcialidad.BackColor = System.Drawing.SystemColors.Control
        Me.lblParcialidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblParcialidad.Location = New System.Drawing.Point(88, 112)
        Me.lblParcialidad.Name = "lblParcialidad"
        Me.lblParcialidad.Size = New System.Drawing.Size(120, 21)
        Me.lblParcialidad.TabIndex = 344
        Me.lblParcialidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumPagos
        '
        Me.lblNumPagos.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumPagos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumPagos.Location = New System.Drawing.Point(88, 80)
        Me.lblNumPagos.Name = "lblNumPagos"
        Me.lblNumPagos.Size = New System.Drawing.Size(124, 21)
        Me.lblNumPagos.TabIndex = 343
        Me.lblNumPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTipoPedido
        '
        Me.lblTipoPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblTipoPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTipoPedido.Location = New System.Drawing.Point(88, 16)
        Me.lblTipoPedido.Name = "lblTipoPedido"
        Me.lblTipoPedido.Size = New System.Drawing.Size(120, 21)
        Me.lblTipoPedido.TabIndex = 342
        Me.lblTipoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(8, 147)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(39, 13)
        Me.Label38.TabIndex = 341
        Me.Label38.Text = " Cada:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(8, 115)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(76, 13)
        Me.Label31.TabIndex = 340
        Me.Label31.Text = " Parcialidades:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(4, 83)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 13)
        Me.Label27.TabIndex = 339
        Me.Label27.Text = " NúmPagos:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(4, 19)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(66, 13)
        Me.Label25.TabIndex = 338
        Me.Label25.Text = " TipoPedido:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(168, 147)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(30, 13)
        Me.Label43.TabIndex = 332
        Me.Label43.Text = " Días"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFolio
        '
        Me.lblFolio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblFolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFolio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolio.Location = New System.Drawing.Point(200, 152)
        Me.lblFolio.Name = "lblFolio"
        Me.lblFolio.Size = New System.Drawing.Size(8, 8)
        Me.lblFolio.TabIndex = 316
        Me.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPedidoReferencia
        '
        Me.lblPedidoReferencia.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedidoReferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedidoReferencia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedidoReferencia.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblPedidoReferencia.Location = New System.Drawing.Point(80, 64)
        Me.lblPedidoReferencia.Name = "lblPedidoReferencia"
        Me.lblPedidoReferencia.Size = New System.Drawing.Size(80, 21)
        Me.lblPedidoReferencia.TabIndex = 327
        Me.lblPedidoReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label20.Location = New System.Drawing.Point(8, 32)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(48, 13)
        Me.Label20.TabIndex = 326
        Me.Label20.Text = "AñoPed:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAñoPed
        '
        Me.lblAñoPed.BackColor = System.Drawing.SystemColors.Control
        Me.lblAñoPed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAñoPed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAñoPed.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblAñoPed.Location = New System.Drawing.Point(80, 32)
        Me.lblAñoPed.Name = "lblAñoPed"
        Me.lblAñoPed.Size = New System.Drawing.Size(80, 21)
        Me.lblAñoPed.TabIndex = 325
        Me.lblAñoPed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHorario
        '
        Me.lblHorario.BackColor = System.Drawing.SystemColors.Control
        Me.lblHorario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHorario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHorario.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblHorario.Location = New System.Drawing.Point(232, 5)
        Me.lblHorario.Name = "lblHorario"
        Me.lblHorario.Size = New System.Drawing.Size(152, 21)
        Me.lblHorario.TabIndex = 324
        Me.lblHorario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label18.Location = New System.Drawing.Point(176, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(49, 13)
        Me.Label18.TabIndex = 323
        Me.Label18.Text = "Horario :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label12.Location = New System.Drawing.Point(8, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 14)
        Me.Label12.TabIndex = 318
        Me.Label12.Text = "Cliente :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContrato
        '
        Me.lblContrato.BackColor = System.Drawing.SystemColors.Control
        Me.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContrato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContrato.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblContrato.Location = New System.Drawing.Point(80, 5)
        Me.lblContrato.Name = "lblContrato"
        Me.lblContrato.Size = New System.Drawing.Size(80, 21)
        Me.lblContrato.TabIndex = 317
        Me.lblContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblStatus.Location = New System.Drawing.Point(80, 128)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(80, 21)
        Me.lblStatus.TabIndex = 314
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label15.Location = New System.Drawing.Point(8, 128)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(45, 13)
        Me.Label15.TabIndex = 315
        Me.Label15.Text = "Status :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Location = New System.Drawing.Point(8, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(33, 13)
        Me.Label16.TabIndex = 313
        Me.Label16.Text = "Folio:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(1424, 616)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(78, 13)
        Me.Label19.TabIndex = 308
        Me.Label19.Text = "Mano de Obra:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(1424, 648)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(40, 13)
        Me.Label21.TabIndex = 307
        Me.Label21.Text = "Monto:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(1424, 552)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(40, 13)
        Me.Label22.TabIndex = 301
        Me.Label22.Text = "Status:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(1424, 520)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(97, 13)
        Me.Label30.TabIndex = 299
        Me.Label30.Text = "Num. Presupuesto:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAyudante
        '
        Me.lblAyudante.BackColor = System.Drawing.SystemColors.Control
        Me.lblAyudante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAyudante.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudante.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblAyudante.Location = New System.Drawing.Point(176, 128)
        Me.lblAyudante.Name = "lblAyudante"
        Me.lblAyudante.Size = New System.Drawing.Size(208, 21)
        Me.lblAyudante.TabIndex = 298
        Me.lblAyudante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTecnico
        '
        Me.lblTecnico.BackColor = System.Drawing.SystemColors.Control
        Me.lblTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTecnico.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTecnico.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblTecnico.Location = New System.Drawing.Point(176, 72)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(208, 21)
        Me.lblTecnico.TabIndex = 297
        Me.lblTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label42.Location = New System.Drawing.Point(176, 104)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(54, 13)
        Me.Label42.TabIndex = 296
        Me.Label42.Text = "Ayudante"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label39.Location = New System.Drawing.Point(176, 56)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(43, 13)
        Me.Label39.TabIndex = 295
        Me.Label39.Text = "Técnico"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnidad
        '
        Me.lblUnidad.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidad.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblUnidad.Location = New System.Drawing.Point(232, 32)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(152, 21)
        Me.lblUnidad.TabIndex = 292
        Me.lblUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFAtencion
        '
        Me.lblFAtencion.BackColor = System.Drawing.SystemColors.Control
        Me.lblFAtencion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFAtencion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAtencion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblFAtencion.Location = New System.Drawing.Point(80, 96)
        Me.lblFAtencion.Name = "lblFAtencion"
        Me.lblFAtencion.Size = New System.Drawing.Size(80, 21)
        Me.lblFAtencion.TabIndex = 290
        Me.lblFAtencion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label41.Location = New System.Drawing.Point(176, 35)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(40, 13)
        Me.Label41.TabIndex = 288
        Me.Label41.Text = "Unidad"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label40.Location = New System.Drawing.Point(8, 96)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(62, 13)
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
        Me.Label26.Size = New System.Drawing.Size(35, 13)
        Me.Label26.TabIndex = 273
        Me.Label26.Text = "Serie:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tpEquipoCliente
        '
        Me.tpEquipoCliente.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpEquipoCliente.Controls.Add(Me.lvwEquipo)
        Me.tpEquipoCliente.Location = New System.Drawing.Point(4, 22)
        Me.tpEquipoCliente.Name = "tpEquipoCliente"
        Me.tpEquipoCliente.Size = New System.Drawing.Size(936, 182)
        Me.tpEquipoCliente.TabIndex = 0
        Me.tpEquipoCliente.Text = "Equipo Cliente"
        Me.tpEquipoCliente.Visible = False
        '
        'lvwEquipo
        '
        Me.lvwEquipo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColSecuencia, Me.ColEquipo, Me.ColTipoPropiedad})
        Me.lvwEquipo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwEquipo.LargeImageList = Me.ImageList1
        Me.lvwEquipo.Location = New System.Drawing.Point(0, 0)
        Me.lvwEquipo.Name = "lvwEquipo"
        Me.lvwEquipo.Size = New System.Drawing.Size(1024, 176)
        Me.lvwEquipo.SmallImageList = Me.ImageList1
        Me.lvwEquipo.TabIndex = 0
        Me.lvwEquipo.UseCompatibleStateImageBehavior = False
        Me.lvwEquipo.View = System.Windows.Forms.View.Details
        '
        'ColSecuencia
        '
        Me.ColSecuencia.Text = "Num. Equipo"
        Me.ColSecuencia.Width = 90
        '
        'ColEquipo
        '
        Me.ColEquipo.Text = "Equipo"
        Me.ColEquipo.Width = 180
        '
        'ColTipoPropiedad
        '
        Me.ColTipoPropiedad.Text = "Propiedad"
        Me.ColTipoPropiedad.Width = 160
        '
        'tpOrdenAutomatica
        '
        Me.tpOrdenAutomatica.BackColor = System.Drawing.Color.YellowGreen
        Me.tpOrdenAutomatica.Controls.Add(Me.GroupBox8)
        Me.tpOrdenAutomatica.Controls.Add(Me.GroupBox7)
        Me.tpOrdenAutomatica.Controls.Add(Me.GroupBox6)
        Me.tpOrdenAutomatica.Location = New System.Drawing.Point(4, 22)
        Me.tpOrdenAutomatica.Name = "tpOrdenAutomatica"
        Me.tpOrdenAutomatica.Size = New System.Drawing.Size(936, 182)
        Me.tpOrdenAutomatica.TabIndex = 5
        Me.tpOrdenAutomatica.Text = "Orden Automática"
        Me.tpOrdenAutomatica.Visible = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.lblTrabajoRealizado)
        Me.GroupBox8.Controls.Add(Me.Label32)
        Me.GroupBox8.Controls.Add(Me.txtTrabajoSolicitado)
        Me.GroupBox8.Controls.Add(Me.Label36)
        Me.GroupBox8.Controls.Add(Me.lblPedido)
        Me.GroupBox8.Controls.Add(Me.Label48)
        Me.GroupBox8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox8.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(224, 168)
        Me.GroupBox8.TabIndex = 371
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "1era. Orden "
        '
        'lblTrabajoRealizado
        '
        Me.lblTrabajoRealizado.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.lblTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrabajoRealizado.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblTrabajoRealizado.Location = New System.Drawing.Point(4, 120)
        Me.lblTrabajoRealizado.Multiline = True
        Me.lblTrabajoRealizado.Name = "lblTrabajoRealizado"
        Me.lblTrabajoRealizado.ReadOnly = True
        Me.lblTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblTrabajoRealizado.Size = New System.Drawing.Size(216, 40)
        Me.lblTrabajoRealizado.TabIndex = 372
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label32.Location = New System.Drawing.Point(4, 104)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(93, 13)
        Me.Label32.TabIndex = 371
        Me.Label32.Text = "Trabajo Realizado"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTrabajoSolicitado
        '
        Me.txtTrabajoSolicitado.BackColor = System.Drawing.SystemColors.Control
        Me.txtTrabajoSolicitado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoSolicitado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoSolicitado.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtTrabajoSolicitado.Location = New System.Drawing.Point(4, 56)
        Me.txtTrabajoSolicitado.Multiline = True
        Me.txtTrabajoSolicitado.Name = "txtTrabajoSolicitado"
        Me.txtTrabajoSolicitado.ReadOnly = True
        Me.txtTrabajoSolicitado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoSolicitado.Size = New System.Drawing.Size(216, 40)
        Me.txtTrabajoSolicitado.TabIndex = 370
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label36.Location = New System.Drawing.Point(4, 40)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(89, 13)
        Me.Label36.TabIndex = 369
        Me.Label36.Text = "TrabajoSolicitado"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPedido
        '
        Me.lblPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedido.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblPedido.Location = New System.Drawing.Point(76, 16)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(144, 21)
        Me.lblPedido.TabIndex = 368
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label48.Location = New System.Drawing.Point(8, 19)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(33, 13)
        Me.Label48.TabIndex = 367
        Me.Label48.Text = "Folio:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblFolioPresupuesto)
        Me.GroupBox7.Controls.Add(Me.lblStatusPresupuesto)
        Me.GroupBox7.Controls.Add(Me.txtObservacionesPres)
        Me.GroupBox7.Controls.Add(Me.Label37)
        Me.GroupBox7.Controls.Add(Me.Label44)
        Me.GroupBox7.Controls.Add(Me.Label45)
        Me.GroupBox7.Controls.Add(Me.lblTot)
        Me.GroupBox7.Controls.Add(Me.lblDesc)
        Me.GroupBox7.Controls.Add(Me.Label46)
        Me.GroupBox7.Controls.Add(Me.Label47)
        Me.GroupBox7.Controls.Add(Me.lblSubT)
        Me.GroupBox7.Controls.Add(Me.Label49)
        Me.GroupBox7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox7.Location = New System.Drawing.Point(232, 8)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(344, 168)
        Me.GroupBox7.TabIndex = 370
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Presupuesto 1er Oden"
        '
        'lblFolioPresupuesto
        '
        Me.lblFolioPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblFolioPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFolioPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolioPresupuesto.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblFolioPresupuesto.Location = New System.Drawing.Point(84, 32)
        Me.lblFolioPresupuesto.Name = "lblFolioPresupuesto"
        Me.lblFolioPresupuesto.Size = New System.Drawing.Size(80, 19)
        Me.lblFolioPresupuesto.TabIndex = 380
        Me.lblFolioPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusPresupuesto
        '
        Me.lblStatusPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusPresupuesto.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblStatusPresupuesto.Location = New System.Drawing.Point(84, 56)
        Me.lblStatusPresupuesto.Name = "lblStatusPresupuesto"
        Me.lblStatusPresupuesto.Size = New System.Drawing.Size(80, 19)
        Me.lblStatusPresupuesto.TabIndex = 379
        Me.lblStatusPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtObservacionesPres
        '
        Me.txtObservacionesPres.BackColor = System.Drawing.SystemColors.Control
        Me.txtObservacionesPres.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesPres.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesPres.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtObservacionesPres.Location = New System.Drawing.Point(180, 48)
        Me.txtObservacionesPres.Multiline = True
        Me.txtObservacionesPres.Name = "txtObservacionesPres"
        Me.txtObservacionesPres.ReadOnly = True
        Me.txtObservacionesPres.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesPres.Size = New System.Drawing.Size(152, 104)
        Me.txtObservacionesPres.TabIndex = 378
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label37.Location = New System.Drawing.Point(180, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(141, 13)
        Me.Label37.TabIndex = 377
        Me.Label37.Text = "Observaciones Presupuesto"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label44.Location = New System.Drawing.Point(4, 106)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(62, 13)
        Me.Label44.TabIndex = 376
        Me.Label44.Text = "Descuento:"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label45.Location = New System.Drawing.Point(4, 130)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(35, 13)
        Me.Label45.TabIndex = 375
        Me.Label45.Text = "Total:"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTot
        '
        Me.lblTot.BackColor = System.Drawing.SystemColors.Control
        Me.lblTot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTot.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTot.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblTot.Location = New System.Drawing.Point(84, 128)
        Me.lblTot.Name = "lblTot"
        Me.lblTot.Size = New System.Drawing.Size(80, 19)
        Me.lblTot.TabIndex = 374
        Me.lblTot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDesc
        '
        Me.lblDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDesc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblDesc.Location = New System.Drawing.Point(84, 104)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(80, 19)
        Me.lblDesc.TabIndex = 373
        Me.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label46.Location = New System.Drawing.Point(4, 58)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(45, 13)
        Me.Label46.TabIndex = 372
        Me.Label46.Text = "Status :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label47.Location = New System.Drawing.Point(4, 34)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(70, 13)
        Me.Label47.TabIndex = 371
        Me.Label47.Text = "Num. Presp.:"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSubT
        '
        Me.lblSubT.BackColor = System.Drawing.SystemColors.Control
        Me.lblSubT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubT.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubT.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblSubT.Location = New System.Drawing.Point(84, 80)
        Me.lblSubT.Name = "lblSubT"
        Me.lblSubT.Size = New System.Drawing.Size(80, 19)
        Me.lblSubT.TabIndex = 370
        Me.lblSubT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label49.Location = New System.Drawing.Point(4, 82)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(53, 13)
        Me.Label49.TabIndex = 369
        Me.Label49.Text = "SubTotal:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblFormaPago)
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.lblTipoServicio)
        Me.GroupBox6.Controls.Add(Me.lblUsuario)
        Me.GroupBox6.Controls.Add(Me.txtTrabSolc)
        Me.GroupBox6.Controls.Add(Me.Label33)
        Me.GroupBox6.Controls.Add(Me.Label34)
        Me.GroupBox6.Controls.Add(Me.Label35)
        Me.GroupBox6.Controls.Add(Me.lblAutoPedido)
        Me.GroupBox6.Controls.Add(Me.Label53)
        Me.GroupBox6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox6.Location = New System.Drawing.Point(584, 8)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(384, 168)
        Me.GroupBox6.TabIndex = 369
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Orden Automática"
        '
        'lblFormaPago
        '
        Me.lblFormaPago.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormaPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFormaPago.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormaPago.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblFormaPago.Location = New System.Drawing.Point(248, 16)
        Me.lblFormaPago.Name = "lblFormaPago"
        Me.lblFormaPago.Size = New System.Drawing.Size(96, 21)
        Me.lblFormaPago.TabIndex = 380
        Me.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label29.Location = New System.Drawing.Point(176, 19)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(68, 13)
        Me.Label29.TabIndex = 379
        Me.Label29.Text = "Forma Pago:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTipoServicio
        '
        Me.lblTipoServicio.BackColor = System.Drawing.SystemColors.Control
        Me.lblTipoServicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTipoServicio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoServicio.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblTipoServicio.Location = New System.Drawing.Point(96, 80)
        Me.lblTipoServicio.Name = "lblTipoServicio"
        Me.lblTipoServicio.Size = New System.Drawing.Size(248, 21)
        Me.lblTipoServicio.TabIndex = 378
        Me.lblTipoServicio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUsuario
        '
        Me.lblUsuario.BackColor = System.Drawing.SystemColors.Control
        Me.lblUsuario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblUsuario.Location = New System.Drawing.Point(96, 48)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(248, 21)
        Me.lblUsuario.TabIndex = 377
        Me.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTrabSolc
        '
        Me.txtTrabSolc.BackColor = System.Drawing.SystemColors.Control
        Me.txtTrabSolc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabSolc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabSolc.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtTrabSolc.Location = New System.Drawing.Point(16, 128)
        Me.txtTrabSolc.Multiline = True
        Me.txtTrabSolc.Name = "txtTrabSolc"
        Me.txtTrabSolc.ReadOnly = True
        Me.txtTrabSolc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabSolc.Size = New System.Drawing.Size(328, 32)
        Me.txtTrabSolc.TabIndex = 376
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label33.Location = New System.Drawing.Point(8, 112)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(92, 13)
        Me.Label33.TabIndex = 375
        Me.Label33.Text = "Trabajo Solicitado"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label34.Location = New System.Drawing.Point(16, 51)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(47, 13)
        Me.Label34.TabIndex = 374
        Me.Label34.Text = "Usuario:"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label35.Location = New System.Drawing.Point(16, 19)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(43, 13)
        Me.Label35.TabIndex = 373
        Me.Label35.Text = "Pedido:"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAutoPedido
        '
        Me.lblAutoPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblAutoPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAutoPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoPedido.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblAutoPedido.Location = New System.Drawing.Point(96, 16)
        Me.lblAutoPedido.Name = "lblAutoPedido"
        Me.lblAutoPedido.Size = New System.Drawing.Size(72, 21)
        Me.lblAutoPedido.TabIndex = 372
        Me.lblAutoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label53.Location = New System.Drawing.Point(16, 83)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(71, 13)
        Me.Label53.TabIndex = 371
        Me.Label53.Text = "Tipo Servicio:"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.YellowGreen
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(736, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 24)
        Me.Label8.TabIndex = 283
        Me.Label8.Text = "Célula:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBitacora
        '
        Me.cboBitacora.BackgroundColor = System.Drawing.SystemColors.Window
        Me.cboBitacora.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cboBitacora.CaptionBackColor = System.Drawing.Color.Yellow
        Me.cboBitacora.CaptionForeColor = System.Drawing.Color.DarkOliveGreen
        Me.cboBitacora.CaptionText = "Bitacora Reprogramación"
        Me.cboBitacora.DataMember = ""
        Me.cboBitacora.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.cboBitacora.Location = New System.Drawing.Point(0, 288)
        Me.cboBitacora.Name = "cboBitacora"
        Me.cboBitacora.ReadOnly = True
        Me.cboBitacora.Size = New System.Drawing.Size(944, 88)
        Me.cboBitacora.TabIndex = 284
        Me.cboBitacora.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.cboBitacora
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCPedido, Me.DGTBCCelula, Me.DGTBCTipoPedidoOriginal, Me.DGTBCFCompromisoOriginal, Me.DGTBCUsuarioOriginal, Me.DGTBCUsuarioReprogramo, Me.DGTBCFCambioReprogramo, Me.DGTBCFCompromisoActual, Me.DGTBCObservacionesReprogramacion})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "Bitacora"
        '
        'DGTBCPedido
        '
        Me.DGTBCPedido.Format = ""
        Me.DGTBCPedido.FormatInfo = Nothing
        Me.DGTBCPedido.HeaderText = "Pedido"
        Me.DGTBCPedido.MappingName = "Pedido"
        Me.DGTBCPedido.Width = 65
        '
        'DGTBCCelula
        '
        Me.DGTBCCelula.Format = ""
        Me.DGTBCCelula.FormatInfo = Nothing
        Me.DGTBCCelula.HeaderText = "Celula"
        Me.DGTBCCelula.MappingName = "Celula"
        Me.DGTBCCelula.Width = 65
        '
        'DGTBCTipoPedidoOriginal
        '
        Me.DGTBCTipoPedidoOriginal.Format = ""
        Me.DGTBCTipoPedidoOriginal.FormatInfo = Nothing
        Me.DGTBCTipoPedidoOriginal.HeaderText = "TipoPedido"
        Me.DGTBCTipoPedidoOriginal.MappingName = "TipoPedidoOriginal"
        Me.DGTBCTipoPedidoOriginal.Width = 95
        '
        'DGTBCFCompromisoOriginal
        '
        Me.DGTBCFCompromisoOriginal.Format = ""
        Me.DGTBCFCompromisoOriginal.FormatInfo = Nothing
        Me.DGTBCFCompromisoOriginal.HeaderText = "FCompromisoOriginal"
        Me.DGTBCFCompromisoOriginal.MappingName = "FCompromisoOriginal"
        Me.DGTBCFCompromisoOriginal.Width = 65
        '
        'DGTBCUsuarioOriginal
        '
        Me.DGTBCUsuarioOriginal.Format = ""
        Me.DGTBCUsuarioOriginal.FormatInfo = Nothing
        Me.DGTBCUsuarioOriginal.HeaderText = "UsuarioOriginal"
        Me.DGTBCUsuarioOriginal.MappingName = "UsuarioOriginal"
        Me.DGTBCUsuarioOriginal.Width = 75
        '
        'DGTBCUsuarioReprogramo
        '
        Me.DGTBCUsuarioReprogramo.Format = ""
        Me.DGTBCUsuarioReprogramo.FormatInfo = Nothing
        Me.DGTBCUsuarioReprogramo.HeaderText = "UsuarioReprogramo"
        Me.DGTBCUsuarioReprogramo.MappingName = "UsuarioReprogramo"
        Me.DGTBCUsuarioReprogramo.Width = 75
        '
        'DGTBCFCambioReprogramo
        '
        Me.DGTBCFCambioReprogramo.Format = ""
        Me.DGTBCFCambioReprogramo.FormatInfo = Nothing
        Me.DGTBCFCambioReprogramo.HeaderText = "FCambioReprogramo"
        Me.DGTBCFCambioReprogramo.MappingName = "FCambioReprogramo"
        Me.DGTBCFCambioReprogramo.Width = 75
        '
        'DGTBCFCompromisoActual
        '
        Me.DGTBCFCompromisoActual.Format = ""
        Me.DGTBCFCompromisoActual.FormatInfo = Nothing
        Me.DGTBCFCompromisoActual.HeaderText = "FCompromisoActual"
        Me.DGTBCFCompromisoActual.MappingName = "FCompromisoActual"
        Me.DGTBCFCompromisoActual.Width = 75
        '
        'DGTBCObservacionesReprogramacion
        '
        Me.DGTBCObservacionesReprogramacion.Format = ""
        Me.DGTBCObservacionesReprogramacion.FormatInfo = Nothing
        Me.DGTBCObservacionesReprogramacion.HeaderText = "Observaciones"
        Me.DGTBCObservacionesReprogramacion.MappingName = "ObservacionesReprogramacion"
        Me.DGTBCObservacionesReprogramacion.Width = 320
        '
        'frmServProgramacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.YellowGreen
        Me.ClientSize = New System.Drawing.Size(1026, 583)
        Me.Controls.Add(Me.cboBitacora)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbLlenaServicioTecnico)
        Me.Controls.Add(Me.lblTotalServicios)
        Me.Controls.Add(Me.lblPuntos)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lvwProgramaciones)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCelula)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.ToolBar2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmServProgramacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Programación"
        Me.tbLlenaServicioTecnico.ResumeLayout(False)
        Me.tpCliente.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.tpOrdenServicio.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.tpServicioTecnico.ResumeLayout(False)
        Me.tpServicioTecnico.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tpEquipoCliente.ResumeLayout(False)
        Me.tpOrdenAutomatica.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.cboBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub frmProgramacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not cnnSigamet Is Nothing Then
            If cnnSigamet.State = ConnectionState.Open Then
                cnnSigamet.Close()
            End If
        End If
        dtpFecha.Value = Now.Date.AddDays(1)
        Llenacelula()
        LlenaLista()
        SumServicios()
        SumPuntos()
        'paintalternatingbackcolor(lvwProgramaciones, Color.CornflowerBlue, Color.White)
        lblFolio.Visible = False
        Dim w As New Check()
    End Sub

    Private Sub ChecaFolio()


        Dim Query As New SqlCommand("select isnull(Folio,0)as Folio from pedido where pedidoreferencia = '" & _Pedido & "' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drFolio As SqlDataReader = Query.ExecuteReader
            While drFolio.Read
                _Folio = CType(drFolio("folio"), Integer)
            End While
            cnnSigamet.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cboCelula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCelula.SelectedIndexChanged
        If DatosCargados Then LlenaLista()
        SumServicios()
        If DatosCargados Then SumPuntos()
        'If DatosCargados Then paintalternatingbackcolor(lvwProgramaciones, Color.CornflowerBlue, Color.White)
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        If DatosCargados Then LlenaLista()
        SumServicios()
        If DatosCargados Then SumPuntos()
        'If DatosCargados Then paintalternatingbackcolor(lvwProgramaciones, Color.CornflowerBlue, Color.White)
    End Sub


    Public Sub ConfiguraConexion()
        Dim Usuario As String
        Dim Password As String
        Usuario = "Franquicia"
        Password = "Franquicia"
        Dim Line As String = Nothing
        Try
            FileOpen(1, Application.StartupPath & "\Login2.inf", OpenMode.Input, OpenAccess.Read)
            Input(1, Line)
            cnSigamet.ConnectionString = Line & "; User id = " & Usuario & "; Password = " & Password
            'cnSigamet.ConnectionString = Line & "Application Name = " & Application.ProductName & " " & Application.ProductVersion & "; User ID = " & UserID & "; Password = " & UserPssw
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error:" & Chr(13) & ex.Message, Application.ProductName & " versión " & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            FileClose(1)
        End Try
    End Sub


    Private Sub ToolBar2_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar2.ButtonClick
        Select Case e.Button.Text
            Case "Reprogramar"

                Cursor = Cursors.WaitCursor
                'If cnSigamet.State = ConnectionState.Open Then
                '    cnSigamet.Close()
                'Else
                'End If
                'ConfiguraConexion()
                'cnSigamet.Open()
                'Dim daChecar As New SqlDataAdapter("select pdd from vwSTExportaServiciosAtendidos WHERE stt = 'ACTIVO' and pdd = " & Pedido & " and apdd = " & AñoPed & " and cll = " & Celula, cnSigamet)
                'Dim dtChecar As New DataTable("ChecaFranquicia")
                'daChecar.Fill(dtChecar)
                'If dtChecar.Rows.Count > 0 Then
                '    MessageBox.Show("El pedido '" & _Pedido & "' no se puede reasignar, pues es pedido de franquicia.", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                If _Estatus = "ACTIVO" Or _Estatus = "PENDIENTE" Then
                    Cursor = Cursors.WaitCursor
                    Dim Reprogramar As New frmReProgramar(Pedido, Celula, AñoPed, GLOBAL_Usuario)
                    Reprogramar.ShowDialog()
                    Cursor = Cursors.Default
                    LlenaLista()
                Else
                    MessageBox.Show("Usted no puede Reprogramar el servicio técnico, pues no tiene status 'ACTIVO'.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                cnSigamet.Close()
                'End If

                Cursor = Cursors.Default



            Case "Asignar"
                Cursor = Cursors.WaitCursor
                'If cnSigamet.State = ConnectionState.Open Then
                '    cnSigamet.Close()
                'Else
                'End If
                'ConfiguraConexion()
                'cnSigamet.Open()
                'Dim daChecar As New SqlDataAdapter("select pdd from vwSTExportaServiciosAtendidos WHERE pdd = " & Pedido & " and apdd = " & AñoPed & " and cll = " & Celula, cnSigamet)

                'Dim dtChecar As New DataTable("ChecaFranquicia")
                'daChecar.Fill(dtChecar)
                'If dtChecar.Rows.Count > 0 Then
                'MessageBox.Show("El Pedido '" & _Pedido & "', no se puede asignar, por que pertenece a una franquicia", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else

                If _Estatus = "ACTIVO" Or _Estatus = "PENDIENTE" Then
                    Cursor = Cursors.WaitCursor
                    Dim Asignar As New frmAsignar(Pedido, Celula, AñoPed, Fcomp, GLOBAL_Usuario)
                    Asignar.ShowDialog()
                    Cursor = Cursors.Default
                    LlenaLista()
                Else
                    MessageBox.Show("Usted no puede asignar el servicio técnico, pues no tiene status 'ACTIVO'.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                'End If

                Cursor = Cursors.Default

                'Texis 
            Case "ST Observación"
                Cursor = Cursors.WaitCursor

                If _Estatus = "ACTIVO" Then
                    Cursor = Cursors.WaitCursor
                    Dim frmDescripcion As New LiquidacionSTN.frmSTObservacion(Pedido, Celula, AñoPed, GLOBAL_Usuario, txtTrabajoRealizado.Text)
                    frmDescripcion.ShowDialog()
                    If (Convert.ToBoolean(frmDescripcion.Tag) = Nothing) Then
                    ElseIf (Convert.ToBoolean(frmDescripcion.Tag) = True) Then
                        MessageBox.Show("Las observaciones se agregaron correctamente.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf (Convert.ToBoolean(frmDescripcion.Tag) = False) Then
                        MessageBox.Show("No se pudo guardar la observacion, Ocurrio un error inesperado.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Else
                        MessageBox.Show("Usted no puede agregar un comentario al servicio técnico, pues no tiene status 'ACTIVO'.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Cursor = Cursors.Default

            Case "Consultar"
                Dim frmConsulta As New LiquidacionSTN.frmConsultar(GLOBAL_Usuario, CelulaUsuario)
                frmConsulta.ShowDialog()

            Case "Liquidar"

                Cursor = Cursors.WaitCursor
                Dim Liq As New LiquidacionSTN.frmLiquidacionST(GLOBAL_Usuario, GLOBAL_Password, GLOBAL_RutaReportes, GLOBAL_Corporativo, GLOBAL_Sucursal, GLOBAL_UsuarioReporte, GLOBAL_PasswordReporte)
                Liq.ShowDialog()
                Cursor = Cursors.Default

            Case "Cancel. Liq."
                    Cursor = Cursors.WaitCursor
                    Dim CancelarLiquidacion As New FrmCancelarLiquidacion(GLOBAL_Usuario)
                    CancelarLiquidacion.ShowDialog()
                    Cursor = Cursors.Default
            Case "Cancel. Ord."

                    Cursor = Cursors.WaitCursor

                Dim _pedidoReferenciaOrden As String = Nothing


                    Dim LlenaLlamada As New SqlCommand("select RTRIM (PedidoReferencia) as PedidoReferencia from pedido p left join serviciotecnico st on p.pedido = st.pedido and p.celula = st.celula and p.añoped = st.añoped where st.franquicia = 1 and StatusServicioTecnico = 'ACTIVO' and pedidoreferencia = '" & _Pedido & "' ", cnnSigamet)
                    Try
                        cnnSigamet.Open()
                        Dim drLlenaLlamada As SqlDataReader = LlenaLlamada.ExecuteReader
                        While drLlenaLlamada.Read
                            _pedidoReferenciaOrden = CType(drLlenaLlamada("PedidoReferencia"), String)

                        End While
                        cnnSigamet.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    If _Pedido = _pedidoReferenciaOrden Then
                        MessageBox.Show("Usted no puede cancelar el servicio pues es una franquicia 'ACTIVA'.", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else

                        If _Estatus = "ACTIVO" Or _Estatus = "PENDIENTE" Then
                            Cursor = Cursors.WaitCursor
                            Dim CancelarOrden As New frmCancelarOrden(Pedido, Celula, AñoPed, GLOBAL_Usuario)
                            CancelarOrden.ShowDialog()
                            Cursor = Cursors.Default
                            LlenaLista()
                        Else
                            MessageBox.Show("Usted no puede cancelar el servicio técnico,pues no tiene status 'ACTIVO'.", "servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                    Cursor = Cursors.Default





            Case "Presupuesto"
                    Cursor = Cursors.WaitCursor
                    If _Estatus = "ACTIVO" Then
                        Cursor = Cursors.WaitCursor
                        _UsaLiquidacion = False
                        Dim Presupuesto As New frmPresupuesto(Pedido, Celula, AñoPed, _UsaLiquidacion)
                        Presupuesto.ShowDialog()
                        Cursor = Cursors.Default
                    Else
                        MessageBox.Show("Usted no puede utilizar esta opción, pues el pedido tiene estatus ATENDIDO.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Cursor = Cursors.Default

            Case "Refrescar"
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    LlenaLista()
                    Cursor = Cursors.Default

            Case "Reporte Prog."

                    Cursor = Cursors.WaitCursor
                    Try
                        Dim folio As Integer
                        folio = 0
                        Dim Reportes As New frmReportesST(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
                        Reportes.Imprime = 1
                        Reportes.ShowDialog()
                    Catch er As Exception
                        MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

                    End Try
                    Cursor = Cursors.Default

            Case "Ciclos"

                    Cursor = Cursors.WaitCursor
                    Dim Ciclos As New GeneraCiclosAutomaticos.frmGeneraCiclos(GLOBAL_Usuario)
                    Ciclos.ShowDialog()
                    Cursor = Cursors.Default


            Case "UnaFranquicia"
                    Cursor = Cursors.WaitCursor
                    If MessageBox.Show("¿Desea usted mandar el pedido '" & _Pedido & "' a una franquicia?.", "Franquicia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ChecaFolio()
                        If _Folio > 0 Then
                            If MessageBox.Show("¿Confirma usted el envio de el pedido '" & _Pedido & "'?", "Franquicia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim daPedido As New SqlDataAdapter("select * from vwSTExtraeInformacionFranquicia where PedidoReferencia =  '" & _Pedido & "'", cnnSigamet)
                                Dim dtpedido As New DataTable("Franquicias")
                                daPedido.Fill(dtpedido)
                                cnnSigamet.Close()

                                If cnSigamet.State = ConnectionState.Open Then
                                    cnSigamet.Close()
                                Else
                                End If

                                ConfiguraConexion()

                                Dim Conexion As SqlConnection = cnSigamet
                                Dim Consulta As DataRow() = dtpedido.Select()
                                Dim dr As DataRow
                                Dim Transaccion As SqlTransaction = Nothing

                                For Each dr In Consulta
                                    Try
                                        cnSigamet.Open()
                                        Dim sqlComando As New SqlCommand()
                                        Transaccion = Conexion.BeginTransaction
                                        sqlComando.Connection = Conexion
                                        sqlComando.Transaction = Transaccion

                                        sqlComando.Parameters.Add("@FRANQUICIA", SqlDbType.Int).Value = dr.Item("Franquicia")
                                        sqlComando.Parameters.Add("@PEDIDO", SqlDbType.Int).Value = dr.Item("Pedido")
                                        sqlComando.Parameters.Add("@AÑOPED", SqlDbType.SmallInt).Value = dr.Item("AñoPed")
                                        sqlComando.Parameters.Add("@CLL", SqlDbType.TinyInt).Value = dr.Item("Celula")
                                        sqlComando.Parameters.Add("@NMCTT", SqlDbType.VarChar).Value = dr.Item("Nombre")
                                        sqlComando.Parameters.Add("@DRRCTT", SqlDbType.VarChar).Value = dr.Item("Direccion")
                                        sqlComando.Parameters.Add("@SVCSLTA", SqlDbType.VarChar).Value = dr.Item("ServicioSolicitado")
                                        sqlComando.Parameters.Add("@FCMPSVC", SqlDbType.DateTime).Value = dr.Item("FCompromiso")
                                        sqlComando.Parameters.Add("@STT", SqlDbType.VarChar).Value = dr.Item("StatusServicioTecnico")
                                        sqlComando.Parameters.Add("@TSVC", SqlDbType.VarChar).Value = dr.Item("TipoServicioDescripcion")
                                        sqlComando.Parameters.Add("@FLPSPT", SqlDbType.Int).Value = dr.Item("FolioPresupuesto")
                                        sqlComando.Parameters.Add("@TSSVCID", SqlDbType.TinyInt).Value = dr.Item("TipoServicio")
                                        sqlComando.Parameters.Add("@CTT", SqlDbType.Int).Value = dr.Item("Cliente")


                                        sqlComando.CommandType = CommandType.StoredProcedure
                                        sqlComando.CommandText = "spSTInsertaFranquicia"
                                        sqlComando.CommandTimeout = 300

                                        sqlComando.ExecuteNonQuery()
                                        Transaccion.Commit()
                                    Catch es As Exception

                                        MessageBox.Show(es.Message)
                                        Transaccion.Rollback()
                                    Finally
                                        Conexion.Close()
                                        'Conexion.Dispose()
                                    End Try


                                Next



                                Dim daPed As New SqlDataAdapter("select Pedido,Celula,Añoped from vwSTExtraeInformacionFranquicia where PedidoReferencia = '" & _Pedido & "'", cnnSigamet)
                                Dim dtPed As New DataTable("Ped")
                                daPed.Fill(dtPed)

                                Dim con As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                Dim Query As DataRow() = dtPed.Select()
                                Dim drPed As DataRow

                                Dim Transacciones As SqlTransaction
                                For Each drPed In Query

                                    Try
                                        con.Open()
                                        Dim Comando As New SqlCommand()
                                        Transacciones = con.BeginTransaction
                                        Comando.Connection = con
                                        Comando.Transaction = Transacciones

                                        Comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = drPed.Item("Pedido")
                                        Comando.Parameters.Add("@Celula", SqlDbType.Int).Value = drPed.Item("celula")
                                        Comando.Parameters.Add("@Añoped", SqlDbType.Int).Value = drPed.Item("AñoPed")


                                        Comando.CommandType = CommandType.Text
                                        Comando.CommandText = "update serviciotecnico set franquicia = 1 where pedido = @Pedido and celula = @Celula and añoped = @AñoPed"
                                        Comando.ExecuteNonQuery()
                                        Transacciones.Commit()
                                    Catch exc As Exception
                                        MessageBox.Show(exc.Message)
                                    Finally
                                        con.Close()
                                        'con.Dispose()
                                    End Try
                                Next

                                MessageBox.Show("Ha terminado el proceso de exportación.", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                LlenaLista()
                            Else
                            End If

                        Else
                            MessageBox.Show("El pedido no esta asignado, debe de asignar primero el pedido", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else

                        Dim Fecha As DateTime
                        Fecha = Now.Date
                        If dtpFecha.Value = Fecha Then
                            MessageBox.Show("Para exportar las franquicias usted debede seleccionar la fecha en que asigno sus pedidos.", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else

                        End If

                    End If

                    Cursor = Cursors.Default

            Case "TFranquicias"

                    Cursor = Cursors.WaitCursor

                    If MessageBox.Show("¿Desea usted mandar las franquicias?.", "Franquicia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ChecaFolio()
                        If _Folio > 0 Then
                            If cnSigamet.State = ConnectionState.Open Then
                                cnSigamet.Close()
                            End If
                            ConfiguraConexion()
                            cnSigamet.Open()
                            Dim da As New SqlDataAdapter("select pdd from vwSTExportaServiciosAtendidos WHERE FCMPSVC BETWEEN '" & dtpFecha.Value.ToShortDateString & "' AND '" & dtpFecha.Value.ToShortDateString & " 23:59:59'", cnSigamet)
                            Dim dt As New DataTable("ChecaFranquicia")
                            cnSigamet.Close()
                            da.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                MessageBox.Show("Usted ya exporto los datos de las franquicias ó selecciono una fecha incorrecta.", "Franquicias", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                Dim daAsig As New SqlDataAdapter("select autotanque,folio from autotanqueturno  where finicioruta between '" & dtpFecha.Value.ToShortDateString & "'and'" & dtpFecha.Value.ToShortDateString & " 23:59:59'and autotanque in (select a.autotanque  from autotanque a  where franquicia is not null) and folio in (select folio from pedido where producto = 4 and fcompromiso = '" & dtpFecha.Value.ToShortDateString & "')", cnnSigamet)
                                Dim dtAsig As New DataTable("ChecaAsignacion")
                                daAsig.Fill(dtAsig)
                                If dtAsig.Rows.Count = 0 Then
                                    MessageBox.Show("Usted no puede exportar los datos de las franquicias por no estar asignadas", "Franquicias", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    If MessageBox.Show("¿Desea usted mandar información a las fraquicias?", "Servicios Técnnico", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                        Dim daExp As New SqlDataAdapter("select * from vwSTExtraeInformacionFranquicia where finicioruta between '" & dtpFecha.Value.ToShortDateString & "'and'" & dtpFecha.Value.ToShortDateString & " 23:59:59' ", cnnSigamet)
                                        Dim dtExp As New DataTable("Franquicias")
                                        daExp.Fill(dtExp)
                                        cnnSigamet.Close()

                                        'If cnSigamet.State = ConnectionState.Open Then
                                        '    cnSigamet.Close()
                                        'Else
                                        'End If

                                        Dim Conexion As SqlConnection = cnSigamet
                                        Dim Transaccion As SqlTransaction
                                        ConfiguraConexion()
                                        cnSigamet.Open()
                                        Transaccion = Conexion.BeginTransaction

                                        Dim Consulta As DataRow() = dtExp.Select()
                                        Dim dr As DataRow

                                        'Dim sqlComando As New SqlCommand()


                                        'sqlComando.Connection = Conexion
                                        'sqlComando.Transaction = Transaccion

                                        For Each dr In Consulta
                                            Try
                                                'cnSigamet.Open()
                                                Dim sqlComando As New SqlCommand()
                                                'Transaccion = Conexion.BeginTransaction
                                                sqlComando.Connection = Conexion
                                                sqlComando.Transaction = Transaccion

                                                sqlComando.Parameters.Add("@FRANQUICIA", SqlDbType.Int).Value = dr.Item("Franquicia")
                                                sqlComando.Parameters.Add("@PEDIDO", SqlDbType.Int).Value = dr.Item("Pedido")
                                                sqlComando.Parameters.Add("@AÑOPED", SqlDbType.SmallInt).Value = dr.Item("AñoPed")
                                                sqlComando.Parameters.Add("@CLL", SqlDbType.TinyInt).Value = dr.Item("Celula")
                                                sqlComando.Parameters.Add("@NMCTT", SqlDbType.VarChar).Value = dr.Item("Nombre")
                                                sqlComando.Parameters.Add("@DRRCTT", SqlDbType.VarChar).Value = dr.Item("Direccion")
                                                sqlComando.Parameters.Add("@SVCSLTA", SqlDbType.VarChar).Value = dr.Item("ServicioSolicitado")
                                                sqlComando.Parameters.Add("@FCMPSVC", SqlDbType.DateTime).Value = dr.Item("FCompromiso")
                                                sqlComando.Parameters.Add("@STT", SqlDbType.VarChar).Value = dr.Item("StatusServicioTecnico")
                                                sqlComando.Parameters.Add("@TSVC", SqlDbType.VarChar).Value = dr.Item("TipoServicioDescripcion")
                                                sqlComando.Parameters.Add("@FLPSPT", SqlDbType.Int).Value = dr.Item("FolioPresupuesto")
                                                sqlComando.Parameters.Add("@TSSVCID", SqlDbType.TinyInt).Value = dr.Item("TipoServicio")
                                                sqlComando.Parameters.Add("@CTT", SqlDbType.Int).Value = dr.Item("Cliente")

                                                sqlComando.CommandType = CommandType.StoredProcedure
                                                sqlComando.CommandText = "spSTInsertaFranquicia"
                                                sqlComando.CommandTimeout = 300

                                                sqlComando.ExecuteNonQuery()
                                                'Transaccion.Commit()
                                            Catch es As Exception
                                                Transaccion.Rollback()
                                                MessageBox.Show("Usted ha tenido problemas con la Exportación de datos.", es.Message)
                                            End Try


                                        Next

                                        Transaccion.Commit()
                                        Conexion.Close()
                                        'Conexion.Dispose()

                                        Dim daFranquicia As New SqlDataAdapter("select Pedido,Celula,Añoped from vwSTExtraeInformacionFranquicia where finicioruta between '" & Now.Date & "'and'" & Now.Date & " 23:59:59' ", cnnSigamet)
                                        Dim dtFranquicia As New DataTable("Franquicias")
                                        daFranquicia.Fill(dtFranquicia)

                                        Dim con As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                        Dim Query As DataRow() = dtFranquicia.Select()
                                        Dim drFranquicia As DataRow

                                        Dim Transacciones As SqlTransaction
                                        For Each drFranquicia In Query

                                            Try
                                                con.Open()
                                                Dim Comando As New SqlCommand()
                                                Transacciones = con.BeginTransaction
                                                Comando.Connection = con
                                                Comando.Transaction = Transacciones

                                                Comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = drFranquicia.Item("Pedido")
                                                Comando.Parameters.Add("@Celula", SqlDbType.Int).Value = drFranquicia.Item("celula")
                                                Comando.Parameters.Add("@Añoped", SqlDbType.Int).Value = drFranquicia.Item("AñoPed")


                                                Comando.CommandType = CommandType.Text
                                                Comando.CommandText = "update serviciotecnico set franquicia = 1 where pedido = @Pedido and celula = @Celula and añoped = @AñoPed"
                                                Comando.ExecuteNonQuery()
                                                Transacciones.Commit()
                                            Catch exc As Exception
                                                MessageBox.Show(exc.Message)
                                            Finally
                                                con.Close()
                                                'con.Dispose()
                                            End Try
                                        Next


                                        MessageBox.Show("Ha terminado el proceso de exportacion", "Servicios Tecnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        LlenaLista()
                                    Else
                                    End If

                                End If
                            End If
                        Else
                            MessageBox.Show("El pedido no esta asignado, debe de asignar primero el pedido", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                    End If

                    Cursor = Cursors.Default


            Case "PantallaFranquicia"

                    Cursor = Cursors.WaitCursor
                    Dim PantallaF As New PantallaFranquicia.frmPantallaFranquicia()
                    PantallaF.ShowDialog()
                    Cursor = Cursors.Default

            Case "Llamada"

                    Cursor = Cursors.WaitCursor
                Dim _pedidoReferenciaLlamada As String = Nothing


                    Dim LlenaLlamada As New SqlCommand("select RTRIM (PedidoReferencia) as PedidoReferencia from pedido p left join serviciotecnico st on p.pedido = st.pedido and p.celula = st.celula and p.añoped = st.añoped where st.llamada = 1 and pedidoreferencia = '" & _Pedido & "' ", cnnSigamet)
                    Try
                        cnnSigamet.Open()
                        Dim drLlenaLlamada As SqlDataReader = LlenaLlamada.ExecuteReader
                        While drLlenaLlamada.Read
                            _pedidoReferenciaLlamada = CType(drLlenaLlamada("PedidoReferencia"), String)

                        End While
                        cnnSigamet.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    If _Pedido = _pedidoReferenciaLlamada Then
                        MessageBox.Show("Usted no puede volver a confirmar el pedido, pues ya esta confirmado", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                    Dim _pedidoReferenciaFranquicia As String = Nothing


                        Dim Llena As New SqlCommand("select RTRIM (PedidoReferencia) as PedidoReferencia from pedido p left join serviciotecnico st on p.pedido = st.pedido and p.celula = st.celula and p.añoped = st.añoped where st.franquicia = 1 and pedidoreferencia = '" & _Pedido & "' ", cnnSigamet)
                        Try
                            cnnSigamet.Open()
                            Dim drLlena As SqlDataReader = Llena.ExecuteReader
                            While drLlena.Read
                                _pedidoReferenciaFranquicia = CType(drLlena("PedidoReferencia"), String)

                            End While
                            cnnSigamet.Close()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        If _Pedido = Trim(_pedidoReferenciaFranquicia) Then
                            If MessageBox.Show("¿Desea confirmar la llamada del pedido?.", "Franquicia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim con As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                con.Open()
                                Dim Comando As New SqlCommand()
                                Dim Transacciones As SqlTransaction
                                Comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                                Comando.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                                Comando.Parameters.Add("@Añoped", SqlDbType.Int).Value = AñoPed
                                Transacciones = con.BeginTransaction
                                Comando.Connection = con
                                Comando.Transaction = Transacciones
                                Try
                                    Comando.CommandType = CommandType.Text
                                    Comando.CommandText = "update serviciotecnico set llamada = 1 where pedido = " & Pedido & " and celula =  " & Celula & " and añoped = " & AñoPed
                                    Comando.ExecuteNonQuery()
                                    Transacciones.Commit()
                                Catch exc As Exception
                                    MessageBox.Show(exc.Message)
                                Finally
                                    con.Close()
                                    'con.Dispose()
                                End Try
                                MessageBox.Show("Usted registro la llamada", "Franquicias", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                LlenaLista()
                            Else
                            End If
                        Else
                        MessageBox.Show("Usted no puede confirmar la llamada a este pedido " & _Pedido & ", pues no es de franquicia.", "Franquicia", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                    Cursor = Cursors.Default

            Case "Cerrar"
                    Me.Close()
        End Select
    End Sub




    Private Sub tpServicioTecnico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpServicioTecnico.Click
        Llenacelula()
        LlenaLista()
    End Sub

    Private Sub MenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem35.Click
        If _Estatus = "ACTIVO" Then
            Cursor = Cursors.WaitCursor
            Dim Reprogramar As New frmReProgramar(Pedido, Celula, AñoPed, GLOBAL_Usuario)
            Reprogramar.ShowDialog()
            Cursor = Cursors.Default
            LlenaLista()
        Else
            MessageBox.Show("Usted no puede Reprogramar el servicio técnico, pues no tiene status 'ACTIVO'.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub MenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem36.Click
        If _Estatus = "ACTIVO" Then
            Cursor = Cursors.WaitCursor
            Dim Asignar As New frmAsignar(Pedido, Celula, AñoPed, Fcomp, GLOBAL_Usuario)
            Asignar.ShowDialog()
            Cursor = Cursors.Default
            LlenaLista()
        Else
            MessageBox.Show("Usted no puede asignar el servicio técnico, pues no tiene status 'ACTIVO'.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub MenuItem48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem48.Click
        Cursor = Cursors.WaitCursor
        Dim Liquidar As New LiquidacionST(_UsuarioClave)
        Liquidar.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub MenuItem38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem38.Click
        Cursor = Cursors.WaitCursor
        Dim CancelarLiquidacion As New FrmCancelarLiquidacion(_UsuarioClave)
        CancelarLiquidacion.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub MenuItem39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem39.Click
        If _Estatus = "ACTIVO" Then
            Cursor = Cursors.WaitCursor
            Dim CancelarOrden As New frmCancelarOrden(Pedido, Celula, AñoPed, _UsuarioClave)
            CancelarOrden.ShowDialog()
            Cursor = Cursors.Default
            LlenaLista()
        Else
            MessageBox.Show("Usted no puede cancelar el servicio técnico,pues no tiene status 'ACTIVO'.", "servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub MenuItem40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem40.Click
        Application.DoEvents()
        LlenaLista()
    End Sub

    Private Sub MenuItem41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem41.Click
        Try
            Dim folio As Integer
            folio = 0
            Dim Reportes As New frmReportesST(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
            Reportes.Imprime = 1
            Reportes.ShowDialog()
        Catch er As Exception
            MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

        End Try
    End Sub

    Private Sub MenuItem42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem42.Click
        Cursor = Cursors.WaitCursor
        Dim Ciclos As New frmCerrarCiclos()
        Ciclos.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub MenuItem43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem43.Click
        Me.Close()
    End Sub

    Private Sub MenuItem45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem45.Click
        Cursor = Cursors.WaitCursor
        Dim CatalogoMaterial As New frmMaterial(GLOBAL_Usuario)
        CatalogoMaterial.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub MenuItem46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Dim CatalogoCreditos As New frmCreditoServicioTecnico(_UsuarioClave)
        CatalogoCreditos.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub MenuItem49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem49.Click
        Try
            Dim folio As Integer
            folio = 0
            Dim Reportes As New frmReportesST(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
            Reportes.Imprime = 5
            Reportes.ShowDialog()
        Catch er As Exception
            MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

        End Try
    End Sub

    Private Sub MenuItem50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem50.Click

        Try
            Dim folio As Integer
            folio = 0
            Dim Reportes As New frmReportesST(dtpFecha.Value, CType(cboCelula.SelectedValue, Integer), folio)
            Reportes.Imprime = 6
            Reportes.ShowDialog()
        Catch er As Exception
            MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

        End Try
    End Sub

    Private Sub MenuItem51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem51.Click
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

    Private Sub MenuItem52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem52.Click
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

    Private Sub MenuItem34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem34.Click

    End Sub

    Private Sub MenuItem44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem44.Click

    End Sub

    Private Sub MenuItem47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem47.Click

    End Sub

    Private Sub MenuItem53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem53.Click
        Cursor = Cursors.WaitCursor
        Dim Equipo As New frmCatalogoEquipo()
        Equipo.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub lblPuntos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPuntos.Click

    End Sub


    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click

    End Sub

    Private Sub lvwProgramaciones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwProgramaciones.SelectedIndexChanged
        Client = lvwProgramaciones.FocusedItem.SubItems(1).Text.Trim
        _Estatus = lvwProgramaciones.FocusedItem.SubItems(6).Text.Trim
        Fcomp = CType(lvwProgramaciones.FocusedItem.SubItems(4).Text.Trim, Date)
        _Pedido = lvwProgramaciones.FocusedItem.SubItems(0).Text.Trim
        LlenaPedido()
        CargaDatosCliente()
        LlenaObservaciones()
        LlenaServiciosTecnicos()
        LlenaPestañaPresupuesto()
        OrdenAutomatica()
        LlenaPagare()
        llenaEquipo()
        LlenaComodato()
        LlenaBitacora()
    End Sub
End Class
