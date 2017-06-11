Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing

Public Class Knob
    Inherits PictureBox

    <Category("Data"), Description("Raised when value has changed.")>
    Public Event OnChange(ByVal Value As Single)

    Private _Minimum As Single = 0.0
    Private _Maximum As Single = 180.0
    Private _Value As Single
    Private _Increment As Single = 1.0

    Public BrushKnobColor As SolidBrush
    Public BrushIndicatorColor As SolidBrush

    Private KnobCenter As Point
    Private IndicatorRadius As Single


    Private _endColor As Color = Color.LimeGreen

    ' The Category attribute tells the designer to display it in the Flash grouping.   
    ' The Description attribute provides a description of the property.   
    <Category("Data"), Description("Get or set the value of the knob.")>
    Public Property Value() As Single
        Get
            Return _Value
        End Get
        Set
            _Value = Value
            RaiseEvent OnChange(Value)
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    <Category("Data"), Description("Get or set the minimum value of the knob.")>
    Public Property Minimum() As Single
        Get
            Return _Minimum
        End Get
        Set
            _Minimum = Value
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    <Category("Data"), Description("Get or set the maximum value of the knob.")>
    Public Property Maximum() As Single
        Get
            Return _Maximum
        End Get
        Set
            _Maximum = Value
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    <Category("Data"), Description("Get or set the increment value of the knob.")>
    Public Property Increment() As Single
        Get
            Return _Increment
        End Get
        Set
            _Increment = Value
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    Sub New()
        'SetStyle()
        BrushKnobColor = New SolidBrush(Color.Gray)
        BrushIndicatorColor = New SolidBrush(Color.Black)

        KnobCenter = New Point()
        IndicatorRadius = 5.0
    End Sub

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        Dim xoffset As Single = Math.Sin(_Value / _Maximum * Math.PI * 2) * (Me.Width / 2 - IndicatorRadius)
        Dim yoffset As Single = -Math.Cos(_Value / _Maximum * Math.PI * 2) * (Me.Height / 2 - IndicatorRadius)

        MyBase.OnPaint(pe)
        ' Draw Knob
        pe.Graphics.FillEllipse(BrushKnobColor, 0, 0, Me.Width, Me.Height)
        ' Draw Indicator
        pe.Graphics.FillEllipse(BrushIndicatorColor, KnobCenter.X - IndicatorRadius + xoffset, KnobCenter.Y - IndicatorRadius + yoffset, IndicatorRadius * 2, IndicatorRadius * 2)

    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        'Debug.Print(e.Delta)
        If e.Delta > 0 Then
            _Value += _Increment
        Else
            _Value -= _Increment
        End If
        'Debug.Print(Value)
        If _Value > _Maximum Then Value = _Maximum
        If _Value < _Minimum Then Value = _Minimum
        Value = _Value
        'Debug.Print(Value)
        'Me.Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        KnobCenter.X = Me.Width / 2
        KnobCenter.Y = Me.Height / 2
    End Sub

End Class
