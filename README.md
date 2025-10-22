# How-to-Serialize-custom-column-in-.NET-MAUI-DataGrid--SfDataGrid

This sample demonstrates how to serialize and deserialize a custom/template column in the Syncfusion .NET MAUI DataGrid (`SfDataGrid`). The Syncfusion DataGrid supports built-in column types (text, numeric, date, etc.), but when you introduce a custom column (for example a template that shows text with an image) you need to extend the grid's serialization pipeline so that your custom column's properties are preserved when the grid is serialized to XML and later restored.


For the official documentation and additional details about the DataGrid serialization API, please refer: [Serialization and Deserialization in .NET MAUI DataGrid (SfDataGrid)](https://help.syncfusion.com/maui/datagrid/serialization)

## What this sample shows

- How to register a custom cell renderer and custom column type in `SfDataGrid`.
- How to provide a custom `DataGridSerializationController` that knows how to convert your custom column to a serializable representation and back.
- How to write/read the serialized XML to a file in the app's local data folder.

## XAML
The sample defines two grids (`dataGrid` and `dataGrid1`) and registers a custom column named `TextImageColumn`. A pair of buttons allow the user to serialize the first grid to a local XML file and then deserialize it into the second grid.

```
<ContentPage.BindingContext>
    <local:EmployeeViewModel x:Name="viewModel" />
</ContentPage.BindingContext>

<Grid ColumnDefinitions="*,300"
        RowDefinitions="Auto,Auto">
    <syncfusion:SfDataGrid x:Name="dataGrid"
                            Grid.Column="0"
                            Grid.Row="0"
                            AllowGroupExpandCollapse="True"
                            ColumnWidthMode="Auto"
                            GridLinesVisibility="Both"
                            HeaderGridLinesVisibility="Both"
                            AutoGenerateColumnsMode="None"
                            ItemsSource="{Binding Employees}">

        <syncfusion:SfDataGrid.Columns>
            <syncfusion:DataGridNumericColumn MappingName="EmployeeID"
                                                HeaderText="Employee ID" />
            <syncfusion:DataGridTextColumn MappingName="Title"
                                            HeaderText="Designation" />
            <syncfusion:DataGridDateColumn MappingName="HireDate"
                                            HeaderText="Hire Date" />
            <local:TextImageColumn MappingName="Title" />

        </syncfusion:SfDataGrid.Columns>
    </syncfusion:SfDataGrid>

    <syncfusion:SfDataGrid x:Name="dataGrid1"
                            Grid.Column="0"
                            Grid.Row="1"
                            ItemsSource="{Binding Employees}">
    </syncfusion:SfDataGrid>

    <VerticalStackLayout Grid.Column="1">
        <Grid RowDefinitions="80,80">
            <Button Text="Serialize"
                    Grid.Row="0"
                    Margin="0,0,0,20"
                    WidthRequest="150"
                    Clicked="Button_Clicked" />
            <Button Text="Deserialize"
                    Grid.Row="1"
                    Margin="0,20,0,0"
                    WidthRequest="150"
                    Clicked="Button_Clicked_1" />
        </Grid>
    </VerticalStackLayout>
</Grid>
```

## C# 

The code registers the custom cell renderer, sets a custom serialization controller and implements a custom serializable column type.

```
dataGrid.CellRenderers.Add("TextImage", new TextImageColumnRenderer());
dataGrid1.CellRenderers.Add("TextImage", new TextImageColumnRenderer());
dataGrid.SerializationController = new SerializationControllerCustomExt(dataGrid);
dataGrid1.SerializationController = new SerializationControllerCustomExt(dataGrid1);

private void Button_Clicked(object sender, EventArgs e)
{
    string localPath = Path.Combine(FileSystem.AppDataDirectory, "DataGrid.xml");
    using (var file = File.Create(localPath))
    {
        dataGrid.Serialize(file);
    }
}

private void Button_Clicked_1(object sender, EventArgs e)
{
    string localPath = Path.Combine(FileSystem.AppDataDirectory, "DataGrid.xml");

    using (var file = File.Open(localPath, FileMode.Open))
    {
        dataGrid1.Deserialize(file);
    }
}
```

## Try it

1. Run the app on your target platform (Android, iOS, Windows, etc.).
2. Tap "Serialize" to write `DataGrid.xml` to the app's local storage.
3. Tap "Deserialize" to read the file and restore the grid definition into the second grid instance.

##### Conclusion
 
I hope you enjoyed learning about how to Serialize template column content in .NET MAUI DataGrid (SfDataGrid).
 
You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. 
For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.
 
If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!
