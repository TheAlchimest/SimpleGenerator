using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The dto class of $Entity$.
/// </summary>
namespace $NameSpace$.Dto
{
    public class $Entity$Dto
    {
$DynamicProperties$

$AdditionalProperties$

    $FormConfiguration$
    }

    public class $Entity$ListFilter : PiginationData
    {
        public string Search { get; set; }
$FilterProperties$
    }
}
