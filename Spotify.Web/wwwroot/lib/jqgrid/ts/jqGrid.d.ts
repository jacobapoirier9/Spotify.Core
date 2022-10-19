interface JqGridCellData {

}



interface JqGridOptions {

    /** applies only to a cell that is editable; this event fires after the edited cell is edited - i.e. after the element is inserted into the DOM */
    afterEditCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => void

    /** applies only to a cell that is editable; this event fires after calling the method restoreCell or the user press ESC leaving the changes */
    afterRestoreCell?: (rowId?: string, value?: any, row?: number, col?: any) => void

    /** applies only to a cell that is editable; this event fires after the cell has been successfully saved. This is the ideal place to change other content. */
    afterSaveCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => void

    /** applies only to a cell that is editable; this event Fires after the cell and other data is posted to the server Should return array of type [success(boolean),message] 
     * when return [true,“”] all is ok and the cellcontent is saved [false,“Error message”] then a dialog appears with the “Error message” and the cell content is not saved. 
     * servereresponse is the response from the server. To use this we should use serverresponse.responseText to obtain the text message from the server. */
    afterSubmitCell?: (response?: any, rowId?: string, colName?: string, value?: any, row?: number, col?: any) => [result: boolean, message: string]

    /** applies only to a cell that is editable; this event fires before editing the cell. */
    beforeEditCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => void

    /** applies only to a cell that is editable; this event fires before validation of values if any. This event can return the new value which value can replace the edited one.
     * The value will be replaced with “new value” */
    beforeSaveCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => any

    /** applies only to a cell that is editable; this event fires before submit the cell content to the server (valid only if cellsubmit : 'remote'). \
     * Can return new object/array that will be posted to the server. The returned object/array will be added to the cellurl posted data. */
    beforeSubmitCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => any

    /** fires if there is a server error; servereresponse is the response from the server. To use this we should apply serverresponse.responseText to obtain 
     * the text message from the server. status is the status of the error. If not set a modal dialog apper. */
    errorCell?: (response?: any, status?) => void

    /** applies only to a cell that is editable; this event allows formatting the cell content before editing, and returns the formatted value */
    formatCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => string

    /** applies only to cells that are not editable; fires after the cell is selected */
    onSelectCell?: (rowId?: string, colName?: string, value?: any, row?: number, col?: any) => void

    /** If set this event can serialize the data passed to the ajax request when we save a cell. The function should return the serialized data. This event can be used 
     * when a custom data should be passed to the server - e.g - JSON string, XML string and etc. To this event is passed the data which will be posted to the server */
    serializeCellData?: (postdata: any) => string








    /** UNKNOWN Can add data to the grid */
    add?: boolean

    /** Caption for the grid */
    caption?: string

    /** Puts the grid into an excel like edit mode */
    cellEdit?: boolean

    /** Where to save changes when a cell is changed (remote | clientArray) */
    cellSubmit?: string

    /** Used with cellSubmit is remote. Where to save the data */
    cellurl?: string

    /** List column models used in the grid */
    colModel?: Array<ColModelOptions>

    /** Datatype / source for grid data (local | json) */
    datatype?: string

    /** UNKNOWN Can delete data from the grid */
    delete?: boolean

    ///** UNKNOWN Can edit data in the grid */
    //edit?: any

    /** Display message when there are no records in the grid */
    emptyrecords?: string

    /** TODO */
    forceFit?: boolean

    /** TODO */
    gridview?: boolean

    /** UNKNOWN */
    grouping?: any

    /** UNKNOWN */
    groupingView?: {

    }

    /** Sets the height of the grid */
    height?: number | string

    /** Prefix for the grid when building grid ids */
    idPrefix?: string

    /** TODO */
    jsonReader?: {
        root?: string
        page?: string
        total?: string
        records?: string
        repeatitems?: boolean
    }

    /** Fires when a grid's data is fully loaded */
    loadComplete?: (gridSummary?: any) => void

    /** Make one request to the server. Subsequent relaods will use local store */
    loadonce?: boolean

    /** HTTP method to be used when retrieving data from the server */
    mtype?: string

    /** UNKNOWN */
    onClickGroup?: (hid?: string, collapsed?: boolean) => void

    /** UNKNOWN */
    onSelectRow?: (rowid?: string, status?: string) => void

    /** DOM Selector of the pager object to be used for the grid */
    pager?: string

    /** Data to be sent along to the server when retrieving data */
    postData?: any

    /** Number of rows to show in one page */
    rowNum?: number

    /** Indicates whether to show row numbers. Will add an extra col model at the beginning of the col model list */
    rownumbers?: boolean

    /** TODO */
    styleUI?: string

    /** OBSOLETE? Indicates whether the grid is searchable */
    search?: boolean

    /** OBSOLTE Indicates whether the grid can be sorted */
    sortable?: boolean

    /** Default column name for sorting */
    sortname?: string

    /** Default direction to sort by sortname (asc | desc) */
    sortorder?: string

    /** OBSOLTE How to read the column used for sorting */
    sorttype?: string | Function

    /** URL used to retrieve data from the server */
    url?: string

    /** TODO */
    viewrecords?: boolean

    /** Width of the grid */
    width?: number | string
}


interface ColModelOptions {

    /** How to align text within the column (left | center | right) */
    align?: string

    editable?: boolean

    /** Mode for editting values .(text | textarea | select | checkbox | password | button | image | file | custom */
    edittype?: string

    /** HTML attributes to be added to the input */
    editoptions?: {
        /** What type of HTML element should be used for these options?
         *  select: string, object or function. Must return something like.. "value:label;value:label"
         *  checkbox: "Option1:Option2" 
         *  else: value for the edit box
         */
        value?: any

        ///** INEFFICIENT with option select, this can be used to get select html from the server via ajax. */
        //dataUrl?: string

        ///** INEFFICIENT if the server can not construct the html, this funciton will using raw data from the server */
        //buildSelect: (data: string) => string

        defaultValue?: any

        /** Send null to the server when a string value is empty */
        NullIfEmpty?: boolean

        /** Any additional attributes to be added to the created element */
        other_options?: any

        custom_element?: (value, editOptions) => HTMLElement

        custom_value?: (obj1, obj2, obj3) => any
    }

    /** How to format the cell. Default is return <span>cellValue</span> */
    formatter?: (
        cellValue?: string,
        info?: {
            rowId?: string
            colModel?: ColModelOptions
            gid?: string
            pos?: number
            styleUI?: string
        },
        model?: any,
        action?: string) => string

    /** Indicates whether this column should be hidden */
    hidden?: boolean

    /** Indicates that htis column should be treated as the key */
    key?: boolean

    /** Label displayed in the header row of the column */
    label?: string

    /** The property name the cell value should read from off the DTO */
    name?: string

    /** Indicates whether this column can be searched */
    search?: boolean

    /** Search operators to be used when searching a column (lt, lte, gt, gte, eq, neq) */
    searchoptions?: SearchOptions

    /** TODO */
    searchrules?: SearchRuleOptions

    /* How to read the value of the column for searching (text | int | integer | float | number | currency | date) */
    stype?: string

    /** Indicates whether this column can be sorted */
    sortable?: boolean

    /** Evaluation type used by the sort engine (text | int | float | date) */
    sorttype?: string | ((cellValue: string, model: any) => number)

    /** Width of the column */
    width?: number
}

interface SearchOptions {

    /** Called when the search is created */
    dataInit?: (element: HTMLElement) => any

    /** Bind HTML events to the search */
    dataEvents?: Array<DataEvents>

    /** HTML attributes to be added to the searched element */
    attr?: any

    /** Include hidden elements in search */
    searchhidden?: boolean

    /**
     * Search operators to be used when searching. This is only used in advanced. 
     * Toolbar and signle field searching will use the first value in the given array.
     * If no values are provided, all of them will be applied.
     * eq = equal, ne = not equal, lt = less than, lte = less than or equal, gt = greater than, gte = greater than or equal
     * bw = begins with, bn = does not begin with, in = is in, ni = is not in
     * ew = ends with, en = does not end with, cn = contains, nc = does not contain
     */
    sopt: Array<string>

    /** Default value used for a cell when no value is present */
    defaultValue?: string

    value?: any

    /** Indicates whether you want to have the ability to clear search */
    clearSearch?: boolean
}

interface SearchRuleOptions {
    required?: boolean

    number?: boolean

    integer?: boolean

    minValue?: number

    maxValue?: number

    email?: boolean

    url?: boolean

    date?: boolean

    time?: boolean

    custom?: boolean
}

interface DataEvents {

    /** HTML Event Type */
    type?: string

    /** Object to attach to the events */
    data?: any

    /** Function called when event is fired. You can use properties from the data object */
    fn?: (events: any) => void
}

interface JqueryStaticForJqGrid {
    (): JQuery
    (method: string, ...params: any[]): any;
    (options: JqGridOptions): JQuery
}

interface JQueryStatic {
    jqGrid?: JqueryStaticForJqGrid
}

interface JQuery {
    jqGrid?: JqueryStaticForJqGrid

    setGridParam(object: any): void
    getGridParam(name: string): any

    addRow(options?: AddRowOptions): JQuery
    editRow(rowId: any, options: EditRowOptions): JQuery

    editCell(row?: number, col?: number, edit?: boolean): JQuery

    /*
     * editCell	iRow, iCol, edit	edit a cell with the row index iRow( do not mix with rowid) in index column iCol. If the edit is set to false the cell is just selected and not edited. If set to true the cell is selected and edited.
getChangedCells	method	Returns an array of the changed cells depending on method (string, default 'all'). When 'all' this method returns all the changed rows; when 'dirty' returns only the changed cells with the id of the row
restoreCell	iRow, iCol	restores the edited content of cell with the row index iRow( do not mix with rowid) in index column iCol
saveCell	iRow, iCol	saves the cell with the row index iRow( do not mix with rowid) in index column iCol
     */
}

interface EditRowOptions {
    keys?: boolean
    oneditfunc?: (domRowId?: string) => void
    successfunc?: (domRowId?: string, response?: CommonOperationResponse) => void
    url?: string
    extraparam?: any
    aftersavefunc?: (domRowId?: string, response?: CommonOperationResponse, model?: any) => void
    errorfunc?: (domRowId?: string, response?: CommonOperationResponse, message?: string) => void
    afterrestorefunc?: (domRowId: string) => void
    restoreAfterError?: boolean
    mtype?: string
}


interface CommonOperationResponse {
    responseText: string
    responseJSON: any
    status: number
    statusText: string
}




interface AddRowOptions {
    rowID?: string
    initdata?: any
    position?: string
    useDefValues?: boolean
    useFormatter?: boolean
    addRowParams?: {
        extraparam?: any
    }
}