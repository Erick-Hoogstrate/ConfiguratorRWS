Module MWriter
    Dim workingGenerationFolder As String
    Dim plantCanvas As CCanvas

    Public Sub Initialize(ByRef newPlantCanvas As CCanvas)
        plantCanvas = newPlantCanvas

        AddHandler ISSDT.GenerateSvgImage.Click, AddressOf GenerateSvgImage_Click
        AddHandler ISSDT.GenerateJSON.Click, AddressOf GenerateJSON_Click
    End Sub


    Private Sub GenerateSvgImage_Click()
        WriteSvgFile(plantCanvas, "Configuration.svg")
    End Sub

    Private Sub GenerateJSON_Click()
        MFileOptions.ExtractJSON("")
    End Sub

    ''' <summary>
    ''' Returns the file location of the models necesary for generation of code.
    ''' </summary>
    ''' <param name="component">The component to get the file location for.</param>
    ''' <returns>The file location that can be appended to the application directory path.</returns>
    Public Function getModelFileLocation(component As CComponent) As String
        Return component.Type.ToString
    End Function


    Public Sub WriteSvgFile(Canvas As CCanvas, outputName As String)
        Dim FileOutput As IO.StreamWriter
        Dim PathDirectory As String = My.Application.Info.DirectoryPath

        GenerateFileRoutine(workingGenerationFolder)

        FileOutput = IO.File.CreateText(System.IO.Path.Combine(workingGenerationFolder, outputName))

        WriteSvgOpening(FileOutput, Canvas.Width, Canvas.Height)

        'Reverse loop in order to keep Z-direction consistent.
        For Each Comp As CComponent In Canvas.Controls.OfType(Of CComponent).Reverse()
            Dim PathInput As String = System.IO.Path.Combine(PathDirectory, String.Format("Components\{0}\Image.svg", getModelFileLocation(Comp)))
            Dim FileInput As IO.StreamReader

            Try
                FileInput = New System.IO.StreamReader(PathInput)
            Catch ex As IO.FileNotFoundException
                MessageBox.Show(ex.Message)
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try

            Dim Line As String
            Line = FileInput.ReadLine()

            While Line IsNot Nothing
                Replacements(Line, Comp)

                FileOutput.WriteLine(Line)
                Line = FileInput.ReadLine()
            End While

            FileInput.Close()
        Next

        WriteSvgClosing(FileOutput)

        FileOutput.Close()
        MsgBox(String.Format("File {0} generated succesfully.", outputName), vbOKOnly, "Succes")

    End Sub

    ''' <summary>
    ''' Replaces all $replacementID$ by a value. 
    ''' </summary>
    ''' <param name="Line">The line in which the $ values have to be replaced.</param>
    Public Sub Replacements(ByRef Line As String, comp As CComponent)
        If Line.Contains("$name$") Then 'CIF compont name, or SVS object ID.
            Line = Line.Replace("$name$", comp.Name)
        End If
        If Line.Contains("$type$") Then 'CIF component type as comment
            Line = Line.Replace("$type$", comp.Type.ToString)
        End If
        If Line.Contains("$X$") Then 'X position of an SVG component.
            Line = Line.Replace("$X$", (comp.Location.X + 0.5 * comp.Width).ToString)
        End If
        If Line.Contains("$Y$") Then 'Y position of an SVG component.
            Line = Line.Replace("$Y$", (comp.Location.Y + 0.5 * comp.Height).ToString)
        End If
        If Line.Contains("$TL_X$") Then 'X position of and SVG square. 
            Line = Line.Replace("$TL_X$", (comp.Location.X).ToString)
        End If
        If Line.Contains("$TL_Y$") Then 'Y position of an SVG square. 
            Line = Line.Replace("$TL_Y$", (comp.Location.Y).ToString)
        End If
        If Line.Contains("$rotation$") Then 'Rotation of an SVG component.
            Line = Line.Replace("$rotation$", (comp.Rotation).ToString)
        End If
        If Line.Contains("$width$") Then 'Width of an SVG component.
            Line = Line.Replace("$width$", comp.Width.ToString)
        End If
        If Line.Contains("$height$") Then 'Height of an SVG component.
            Line = Line.Replace("$height$", comp.Height.ToString)
        End If
        If Line.Contains("$color$") Then 'Color of an svg component in HEX.
            Line = Line.Replace("$color$", Hex(comp.BackColor.R) + Hex(comp.BackColor.G) + Hex(comp.BackColor.B))
        End If
        If Line.Contains("$filename$") Then 'File name for svg declaration statements.
            Line = Line.Replace("$filename$", """plant.svg""")
        End If
    End Sub

    Public Sub WriteSvgOpening(fileSvgImage As IO.StreamWriter, plantCanvasWidth As Integer, plantCanvasHeight As Integer)
        fileSvgImage.WriteLine("<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>")
        fileSvgImage.WriteLine("<!-- Created with Infrastructural System Design Tool -->")
        fileSvgImage.WriteLine("")
        fileSvgImage.WriteLine("<svg")
        fileSvgImage.WriteLine("   xmlns:dc=""http://purl.org/dc/elements/1.1/""")
        fileSvgImage.WriteLine("   xmlns:cc=""http://creativecommons.org/ns#""")
        fileSvgImage.WriteLine("   xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""")
        fileSvgImage.WriteLine("   xmlns:svg=""http://www.w3.org/2000/svg""")
        fileSvgImage.WriteLine("   xmlns=""http://www.w3.org/2000/svg""")
        fileSvgImage.WriteLine("   xmlns:sodipodi=""http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd""")
        fileSvgImage.WriteLine("   xmlns:inkscape=""http://www.inkscape.org/namespaces/inkscape""")
        fileSvgImage.WriteLine("   width=""" + plantCanvasWidth.ToString + """")
        fileSvgImage.WriteLine("   height=""" + plantCanvasHeight.ToString + """")
        fileSvgImage.WriteLine("   viewBox=""""")
        fileSvgImage.WriteLine("   version=""1.1""")
        fileSvgImage.WriteLine("   id=""blank""")
        fileSvgImage.WriteLine("   inkscape:version=""0.92.3 (2405546, 2018-03-11)""")
        fileSvgImage.WriteLine("   sodipodi:docname=""Img.svg"">")
        fileSvgImage.WriteLine("  <defs")
        fileSvgImage.WriteLine("     id=""defs867"" />")
        fileSvgImage.WriteLine("  <sodipodi:namedview")
        fileSvgImage.WriteLine("     id=""base""")
        fileSvgImage.WriteLine("     pagecolor=""#ffffff""")
        fileSvgImage.WriteLine("     bordercolor=""#666666""")
        fileSvgImage.WriteLine("     borderopacity=""1.0""")
        fileSvgImage.WriteLine("     inkscape:pageopacity=""0.0""")
        fileSvgImage.WriteLine("     inkscape:pageshadow=""2""")
        fileSvgImage.WriteLine("     inkscape:zoom=""0.64""")
        fileSvgImage.WriteLine("     inkscape:cx=""226""")
        fileSvgImage.WriteLine("     inkscape:cy=""476.13687""")
        fileSvgImage.WriteLine("     inkscape:document-units=""mm""")
        fileSvgImage.WriteLine("     inkscape:current-layer=""blank""")
        fileSvgImage.WriteLine("     showgrid=""false""")
        fileSvgImage.WriteLine("     units=""px""")
        fileSvgImage.WriteLine("     inkscape:window-width=""1920""")
        fileSvgImage.WriteLine("     inkscape:window-height=""1137""")
        fileSvgImage.WriteLine("     inkscape:window-x=""-8""")
        fileSvgImage.WriteLine("     inkscape:window-y=""-8""")
        fileSvgImage.WriteLine("     inkscape:window-maximized=""1"" />")
        fileSvgImage.WriteLine("  <metadata")
        fileSvgImage.WriteLine("     id=""metadata870"">")
        fileSvgImage.WriteLine("    <rdf:RDF>")
        fileSvgImage.WriteLine("      <cc:Work")
        fileSvgImage.WriteLine("         rdf:about="""">")
        fileSvgImage.WriteLine("        <dc:format>image/svg+xml</dc:format>")
        fileSvgImage.WriteLine("        <dc:type")
        fileSvgImage.WriteLine("           rdf:resource=""http://purl.org/dc/dcmitype/StillImage"" />")
        fileSvgImage.WriteLine("        <dc:title></dc:title>")
        fileSvgImage.WriteLine("      </cc:Work>")
        fileSvgImage.WriteLine("    </rdf:RDF>")
        fileSvgImage.WriteLine("  </metadata>")
        fileSvgImage.WriteLine("  <g")
        fileSvgImage.WriteLine("     inkscape:Label =""Components""")
        fileSvgImage.WriteLine("     inkscape:groupmode =""layer""")
        fileSvgImage.WriteLine("     id=""Components"" />")
    End Sub

    Public Sub WriteSvgClosing(fileSvgImage As IO.StreamWriter)
        fileSvgImage.WriteLine("</svg>")
    End Sub

    Public Sub GenerateFileRoutine(folderName As String)
        If String.IsNullOrEmpty(folderName) Then
            ISSDT.FolderBrowserDialog.ShowDialog()
            workingGenerationFolder = ISSDT.FolderBrowserDialog.SelectedPath
            If String.IsNullOrEmpty(workingGenerationFolder) Then Exit Sub
        End If
    End Sub
End Module
