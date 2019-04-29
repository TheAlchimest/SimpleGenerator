using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMOSampleInConsol
{
    public enum EnumInputType
    {
        undefined,
        textarea,
        select,
        checkbox,       // Defines a checkbox
        date,           // Defines a date control (year, month, day (no time))
        email,          //Defines a field for an e-mail address
        file,           // Defines a file-select field and a "Browse" button (for file uploads)
        
        UploadedServerfileWithJustNameInDB,           //this will add a file and still just and name to database
        UploadedServerPhotoWithThumbnailAndJustNameInDB,           //this will add a file and still just and name to database
        hidden,         //  Defines a hidden input field
        number,         //  Defines a field for entering a number
        password,           // Defines a password field
        radio,          //Defines a radio button
        text,           // Default. Defines a single-line text field
        url,          // Defines a field for entering a URL
        tel,
        checkboxList,
        button,         //Defines a clickable button (mostly used with a JavaScript to activate a script)
        color,          //Defines a color picker
        datetime,  //  Defines a date and time control (year, month, day, time (no timezone)
        image,//Defines an image as the submit button
        month,//Defines a month and year control (no timezone)
        range,//Defines a range control (like a slider control)
        reset,//Defines a reset button
        search,//  Defines a text field for entering a search string
        submit,//  Defines a submit button
        time,// Defines a control for entering a time (no timezone)
        week,// Defines a week and year control (no timezone)
        postalCode,
        creditCard
    }
}
