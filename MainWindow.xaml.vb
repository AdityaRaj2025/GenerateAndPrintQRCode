Imports QRCoder
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Media.Imaging

Public Class MainWindow
    Private Sub PrintQRCode_Click(sender As Object, e As RoutedEventArgs)
        Dim qrData As String = txtQRData.Text

        If Not String.IsNullOrEmpty(qrData) Then
            GenerateAndPrintQRCode(qrData)
        End If
    End Sub

    Private Sub GenerateAndPrintQRCode(data As String)
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)

        Dim qrCodeImage As Bitmap = qrCode.GetGraphic(10) ' Adjust the size as needed

        Dim qrCodeBitmap As BitmapImage = ConvertToBitmapImage(qrCodeImage)
        imgQRCode.Source = qrCodeBitmap

        Dim printDialog As New PrintDialog()
        If printDialog.ShowDialog() = True Then
            printDialog.PrintVisual(imgQRCode, "QR Code Print")
        End If
    End Sub

    Private Function ConvertToBitmapImage(bitmap As Bitmap) As BitmapImage
        Dim bitmapImage As New BitmapImage()
        Using memory As New MemoryStream()
            bitmap.Save(memory, ImageFormat.Png)
            memory.Position = 0
            bitmapImage.BeginInit()
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad
            bitmapImage.StreamSource = memory
            bitmapImage.EndInit()
        End Using
        Return bitmapImage
    End Function

End Class
