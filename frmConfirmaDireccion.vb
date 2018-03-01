Public Class frmConfirmaDireccion
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Cliente As Integer)
        MyBase.New()
        _Cliente = Cliente

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents SeleccionCalleColonia1 As SigaMetClasses.SeleccionCalleColonia
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmConfirmaDireccion))
        Me.SeleccionCalleColonia1 = New SigaMetClasses.SeleccionCalleColonia()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'SeleccionCalleColonia1
        '
        Me.SeleccionCalleColonia1.Calle = 0
        Me.SeleccionCalleColonia1.Colonia = 0
        Me.SeleccionCalleColonia1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeleccionCalleColonia1.Location = New System.Drawing.Point(8, 8)
        Me.SeleccionCalleColonia1.Name = "SeleccionCalleColonia1"
        Me.SeleccionCalleColonia1.Size = New System.Drawing.Size(536, 144)
        Me.SeleccionCalleColonia1.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(600, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(600, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 2
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmConfirmaDireccion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(688, 166)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCancelar, Me.btnAceptar, Me.SeleccionCalleColonia1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmConfirmaDireccion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmConfirmaDireccion"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmConfirmaDireccion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SeleccionCalleColonia1.CargaDatosCliente(_Cliente)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Calle = SeleccionCalleColonia1.Calle
        Colonia = SeleccionCalleColonia1.Colonia
        NumExterior = CType(SeleccionCalleColonia1.NumExterior, String)
        If SeleccionCalleColonia1.NumInterior Is "" Then
            NumInterior = CType(0, String)
        Else
            NumInterior = CType(SeleccionCalleColonia1.NumInterior, String)
        End If
        _Folio = "si"
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub SeleccionCalleColonia1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionCalleColonia1.Load

    End Sub
End Class
