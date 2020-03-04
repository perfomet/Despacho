using Microsoft.VisualBasic;
using System;
using System.Data;
using Datos.DB;
using System.Data.SqlClient;

namespace Datos.Datos
{
	//public static SqlConnection ConexionSQL;

	public static class Internos
	{
		public const int sdominicana = 1;
		public const int schile = 0;
		public const int snumero = 0;
		public const int stexto = 1;

		public static bool IsNullOrEmpty(string s)
		{
			bool result;
			result = s == null || s == string.Empty;
			return result;
		}
		public static Boolean IsDate(String fecha)
		{
			try
			{
				DateTime.Parse(fecha);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public static bool RelacionFechaSolicitudFechaRecepcion(string FechaSolicitud, string FechaRecepcion)
		{
			DateTime fechasolicitud = DateTime.Parse(FechaSolicitud);
			DateTime fecharecepcion = DateTime.Parse(FechaSolicitud);
					 
			return ((fechasolicitud - fecharecepcion).Ticks >= 0);
		}
		public static bool CorrespondeaRegion(string NombreComuna, string NombreRegion)
			{
			string SELECTSentence = "SELECT Region.Region";
			string FROMSentence = " FROM Comuna";
			string JOINSentence=" INNER JOIN Region ON Comuna.RegionId = Region.RegionId";
			string WHERESentence = " WHERE (Comuna.Comuna like '"+ NombreComuna.ToUpper() + ")' AND (Region.Region LIKE '" + NombreRegion.ToUpper() + "'))";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			return (dataTable.Rows.Count == 1);
			}
		public static bool ExisteContenido(string TableName, string FieldName, string Content, int tipo)
		{
			int filas = 0;
			string SELECTSentence = "";
			string FROMSentence = "";
			string WHERESentence = "";
			if (FieldName == "Placa"){
				SELECTSentence = "SELECT [MiLogistic].[dbo]." + TableName + "." + FieldName;
				FROMSentence = " FROM [MiLogistic].[dbo]." + TableName;
				WHERESentence = " WHERE [MiLogistic].[dbo]." + TableName + "." + FieldName;
			}
			else { 
				SELECTSentence = "SELECT " + TableName + "." + FieldName;
				FROMSentence = " FROM " + TableName;
				WHERESentence = " WHERE " + TableName + "." + FieldName;
			}
			if (tipo==snumero)
				{
					WHERESentence = WHERESentence + " = " + Content;
				}
					else
				{
					if (FieldName == "Placa")
					{
						WHERESentence = WHERESentence + " LIKE '" + Content.ToUpper() + "' OR " + "[MiLogistic].[dbo]."+ TableName + "." + FieldName + " LIKE '" + Content + "'";
					}
					else
					{
						WHERESentence = WHERESentence + " LIKE '" + Content.ToUpper() + "' OR " + TableName + "." + FieldName + " LIKE '" + Content + "'";
					}
					
				}
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			filas = dataTable.Rows.Count;
			return ( filas == 1);

		}
		public static bool Existe(int Valor)
		{
			return true;
		}
		public static bool ExisteTabla(string nombretabla)
		{
			string SELECTSentence = "SELECT COUNT(*)";
			string FROMSentence = " FROM INFORMATION_SCHEMA.TABLES "; 
			string WHERESentence=" WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = @nombreTabla";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			return (dataTable.Rows.Count > 0);
		}
		public static bool CreateTable(string nametable)
		{
			if (ExisteTabla(nametable) == true)
			{
				return false;
			}
			else
			{
				return true;
			}
			
		}
		public static int CuentaCaracteres(string texto, string caracter)
		{
			return texto.Length - texto.Replace(caracter, "").Length;
		}
		public static string Verirut(string nrut, int dedonde)
		{
			object totsum;
			object rutvariant;
			object temp = null;

			int idif;
			int sconst;
			object[] digito = new object[10];
			int tope = 9;
			int idigital;
			int t = 0;
			int r;
			string resultado = null;
			// el Rut está en la tabla como 999.999.999,
			// ejemplo 13.087.314-6
			rutvariant = nrut.Replace(".", "").Trim();
			rutvariant = rutvariant.ToString().Replace(",", "");
			switch (dedonde)
			{
				case schile:
					//sconst = 432765432;
					{
						if ((Strings.InStr(rutvariant.ToString(), "-") > 0))
							rutvariant = Strings.Left(rutvariant.ToString(), Strings.InStr(rutvariant.ToString(), "-") - 1);
						else
						{
						}
						break;
					}
				case sdominicana:
					{
						switch (CuentaCaracteres(rutvariant.ToString(), "-"))
						{
							case 1:
								{
									rutvariant = Strings.Replace(rutvariant.ToString(), "-", "");
									break;
								}

							case 2:
								{
									rutvariant = Strings.Left(rutvariant.ToString(), (rutvariant.ToString().Length) - 1);
									idigital = Convert.ToInt32(Strings.Right(nrut, 1));
									break;
								}
						}
						rutvariant = rutvariant.ToString().Replace("-", "").Trim();
						break;
					}

				default:
					{
						break;
					}
			}
			if (!Information.IsNumeric(rutvariant.ToString()) & (rutvariant.ToString() != ""))
			{
				//MsgBox("Está empleando caracteres no permitidos", (MsgBoxStyle)Constants.vbOKOnly + Constants.vbCritical, Asistente);//
				return null;
			}
			else
				// If (CBool(InStr(nrut, "-")) And (dedonde = sPaisAsiento)) Then
				// rutvariant = Left(rutvariant.ToString, rutvariant.ToString.Length - 1)
				// Else
				// End If
				switch (dedonde)
				{
					case schile:
						{

							if ((Information.IsDBNull(rutvariant) | (rutvariant.ToString() == "")))
								return null;
							else if (Information.IsDBNull(rutvariant.ToString().Length))
								return null;
							else
							{
								for (int i = 1; i <= rutvariant.ToString().Length; i++)
								{
									if (Information.IsNumeric(Strings.Mid(rutvariant.ToString(), i, 1)))
									{
										if (Information.IsNothing(temp))
											temp = Strings.Mid(rutvariant.ToString(), i, 1);
										else
											temp = temp.ToString() + Strings.Mid(rutvariant.ToString(), i, 1);
									}
									else
									{
									}
								}
								idif = tope - Convert.ToString(temp).Length;
								if ((idif != 0))
								{
									for (int i = 1; i <= idif; i++)
										temp = " " + temp.ToString();
								}
								for (int i = 1; i <= tope; i++)
									digito[i] = Conversion.Val(Strings.Mid(temp.ToString(), i, 1));
								totsum = Convert.ToInt32(digito[1]) * 4 + Convert.ToInt32(digito[2]) * 3 + Convert.ToInt32(digito[3]) * 2 + Convert.ToInt32(digito[4]) * 7 + Convert.ToInt32(digito[5]) * 6 + Convert.ToInt32(digito[6]) * 5 + Convert.ToInt32(digito[7]) * 4 + Convert.ToInt32(digito[8]) * 3 + Convert.ToInt32(digito[9]) * 2;
								resultado = Convert.ToString((Conversion.Int(Convert.ToDouble(totsum) / (double)11) + 1) * 11 - Convert.ToDouble(totsum));
								switch (Convert.ToInt32(resultado))
								{
									case 10:
										{
											return "K";
										}

									case 11:
										{
											return Convert.ToString(0);
										}

									default:
										{
											return Strings.Left(resultado.ToString(), 1);
										}
								}
							}

							/*break;*/
						}

					case sdominicana:
						{
							sconst = 1212121212;
							if ((rutvariant.ToString().Length < 10))
								return null;
							else
							{
								resultado = "0";
								// idigital = CInt(Right(rutvariant.ToString, 1))
								for (int i = 0; i <= tope; i++)
								{
									totsum = Convert.ToInt32(rutvariant.ToString().Substring(i, 1)) * Convert.ToInt32(sconst.ToString().Substring(i, 1));
									if ((Convert.ToInt32(totsum) > 9))
									{
										totsum = Convert.ToInt32(totsum) - 10 + 1;
									}
									else
									{

									}
									t = t + Convert.ToInt32(totsum);
								}
								resultado = Convert.ToString((10 - (t % 10)) % 10);
							}
							return Convert.ToString(resultado);
						}

					default:
						{
							tope = 7;
							sconst = 79865432;
							if ((Convert.ToString(rutvariant).Length < 9))
								return null;
							idigital = Convert.ToInt32(Strings.Right(rutvariant.ToString(), 1));
							for (int i = 0; i <= tope; i++)
							{
								totsum = Convert.ToInt32(rutvariant.ToString().Substring(i, 1)) * Convert.ToInt32(sconst.ToString().Substring(i, 1));
								t = t + Convert.ToInt32(totsum);
							}
							r = t % 11;
							resultado = Convert.ToString(11 - r);
							switch (r)
							{
								case 0:
									{
										resultado = "2";
										break;
									}

								case 1:
									{
										resultado = "1";
										break;
									}
							}
							return Convert.ToString(resultado);
						}
				}
		}
	}
}
