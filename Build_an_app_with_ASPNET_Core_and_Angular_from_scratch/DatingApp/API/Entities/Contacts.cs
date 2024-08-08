using System.ComponentModel.DataAnnotations.Schema;

namespace API;

public class Contacts
{
    [Column("t_ccnt")]
    public string ContactCode { get; set; }
    [Column("t_ctit")]
    public string TitleCode { get; set; }
    [Column("t_init")]
    public string Initials { get; set; }
    [Column("t_post")]
    public string Suffix { get; set; }
    [Column("t_inse")]
    public string FirstName { get; set; }
    [Column("t_surn")]
    public string LastName { get; set; }
    [Column("t_seak")]
    public string SearchKey { get; set; }
    [Column("t_bday.c")]
    public DateOnly BirthDate { get; set; }
    [Column("t_clan")]
    public string LanguageCode { get; set; }
    [Column("t_crnm.c")]
    public string CreatedBy { get; set; }
    [Column("t_crdt.c")]
    public DateTime CreationDate { get; set; }
    [Column("t_dtlm")]
    public DateTime LastModifed { get; set; }
    [Column("t_mdnm.c")]
    public string LastModifedBy { get; set; }
    [Column("t_telp")]
    public string Telephone { get; set; }
    [Column("t_tfcd.c")]
    public string CountryCode { get; set; }
    [Column("t_cell.c")]
    public string CellNumber { get; set; }
    [Column("t_teld")]
    public string DirectDail { get; set; }
    [Column("t_tefx")]
    public string Fax { get; set; }
    [Column("t_info")]
    public string Email { get; set; }
}
