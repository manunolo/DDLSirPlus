using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ManagerSirPlus
{
	public class ModelsSirPlus
	{
		public DateTime fecha_sincronizacion { get; set; }
		public int id_sincronizacion { get; set; }
		public ClienteClass cliente { get; set; }
		public List<CuponRecepcionClass> cupones { get; set; }
		public int cant_cupones { get; set; }
		public int total_recepcion { get; set; }
	}
	public class ClienteClass
	{
		public int numero { get; set; }
		public string razon_social { get; set; }
		public int cuit { get; set; }
		public string password { get; set; }
	}
	public class CuponRecepcionClass
	{
		public DateTime fecha { get; set; }
		public int anio { get; set; }
		public int periodo { get; set; }
		public int numero_cupon { get; set; }
		public int numero_cliente { get; set; }
		public string razon_social_cliente { get; set; }
		public string mail { get; set; }
		public int? numero_cuenta { get; set; }
		public int cbu { get; set; }
		public string titular_cuenta { get; set; }
		public int numero_cuenta_empresa { get; set; }
		public int? cbu_empresa { get; set; }
		public string titular_cuenta_empresa { get; set; }
		public float total { get; set; }
		public int id_moneda_sirplus { get; set; }
		public float valor_pesos { get; set; }
		public int codigo_barras { get; set; }
		public int numero_cuenta_inmobiliaria { get; set; }
		public int cbu_inmobiliaria { get; set; }
		public int titular_cuenta_inmobiliaria { get; set; }
		public List<VencimientoClass> vencimientos { get; set; }
		public List<EntidadClass> entidades { get; set; }
	}
	public class VencimientoClass
	{
		public DateTime fecha_vencimiento { get; set; }
		public float importe { get; set; }
		public int id_moneda_vencimiento_sirplus { get; set; }
		public float valor_pesos_vencimiento { get; set; }
	}
	public class EntidadClass
	{
		public int id_entidad { get; set; }
	}
	public class LiquidacionClass
	{
		public int id_liquidacion { get; set; }
		public int numero_liquidacion { get; set; }
		public int id_cliente { get; set; }
		public DateTime fecha { get; set; }
		public float total_cupones { get; set; }
		public float total_comision { get; set; }
		public float total_conceptos { get; set; }
		public float total_liquidado { get; set; }
		public string estado { get; set; }
		public List<CuponLiquidacionClass> cupones { get; set; }
		public List<ConceptoClass> conceptos { get; set; }
	}
	public class CuponLiquidacionClass
	{
		public int id_cupon { get; set; }
		public int numero_cupon { get; set; }
		public int codigo_barras { get; set; }
		public DateTime fecha_cupon { get; set; }
		public int id_moneda { get; set; }
		public int importe_cobrado { get; set; }
		public int valor_pesos_cobrado { get; set; }
		public int id_entidad { get; set; }
		public int importe_comision { get; set; }
		public DateTime fecha_cobro { get; set; }
	}
	public class ConceptoClass
	{
		public int id_concepto { get; set; }
		public int id_moneda_concepto { get; set; }
		public string descripcion_concepto { get; set; }
		public int importe_concepto { get; set; }
		public int valor_pesos_concepto { get; set; }
	}
	public class Manager
	{
		public static void CreacionArchivoXML(List<ModelsSirPlus> recepciones, string ubicacion, string nombreArchivo)
		{
			XmlWriter xmlWriter = XmlWriter.Create(ubicacion + "/" + nombreArchivo + ".xml");

			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("recepciones");
			foreach (ModelsSirPlus recepcion in recepciones)
			{
				xmlWriter.WriteStartElement("recepcion");
				xmlWriter.WriteElementString("fecha_sincronizacion", recepcion.fecha_sincronizacion.ToString());
				xmlWriter.WriteElementString("id_sincronizacion", recepcion.id_sincronizacion.ToString());
				xmlWriter.WriteStartElement("Cliente");
				xmlWriter.WriteElementString("numero", recepcion.cliente.numero.ToString());
				xmlWriter.WriteElementString("razon_social", recepcion.cliente.razon_social);
				xmlWriter.WriteElementString("cuit", recepcion.cliente.cuit.ToString());
				xmlWriter.WriteElementString("password", recepcion.cliente.password);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("cupones");
				foreach (var cupon in recepcion.cupones)
				{
					xmlWriter.WriteStartElement("cupon");
					xmlWriter.WriteElementString("fecha", cupon.fecha.ToString());
					xmlWriter.WriteElementString("numero_cupon", cupon.numero_cupon.ToString());
					xmlWriter.WriteElementString("numero_cliente", cupon.numero_cliente.ToString());
					xmlWriter.WriteElementString("razon_social_cliente", cupon.razon_social_cliente);
					xmlWriter.WriteElementString("mail", cupon.mail);
					xmlWriter.WriteElementString("total", cupon.total.ToString());
					xmlWriter.WriteElementString("id_moneda_sirplus", cupon.id_moneda_sirplus.ToString());
					xmlWriter.WriteElementString("valor_pesos", cupon.valor_pesos.ToString());
					xmlWriter.WriteElementString("codigo_barras", cupon.codigo_barras.ToString());
					xmlWriter.WriteElementString("numero_cuenta", cupon.numero_cuenta.ToString());
					xmlWriter.WriteElementString("cbu", cupon.cbu.ToString());
					xmlWriter.WriteElementString("titular_cuenta", cupon.titular_cuenta);
					xmlWriter.WriteElementString("periodo", cupon.periodo.ToString());
					xmlWriter.WriteElementString("anio", cupon.anio.ToString());
					xmlWriter.WriteElementString("numero_cuenta_inmobiliaria", cupon.numero_cuenta_inmobiliaria.ToString());
					xmlWriter.WriteElementString("cbu_inmobiliaria", cupon.cbu_inmobiliaria.ToString());
					xmlWriter.WriteElementString("titular_cuenta_inmobiliaria", cupon.titular_cuenta_inmobiliaria.ToString());
					foreach (var vencimiento in cupon.vencimientos)
					{
						xmlWriter.WriteStartElement("vencimientos");
						xmlWriter.WriteElementString("fecha_vencimiento", vencimiento.fecha_vencimiento.ToString());
						xmlWriter.WriteElementString("importe", vencimiento.importe.ToString());
						xmlWriter.WriteElementString("id_moneda_vencimiento_sirplus", vencimiento.id_moneda_vencimiento_sirplus.ToString());
						xmlWriter.WriteElementString("valor_pesos_vencimiento", vencimiento.valor_pesos_vencimiento.ToString());
						xmlWriter.WriteEndElement();
					}
					xmlWriter.WriteStartElement("entidades");
					foreach (var entidad in cupon.entidades)
					{
						xmlWriter.WriteStartElement("entidad");
						xmlWriter.WriteElementString("id_entidad", entidad.id_entidad.ToString());
						xmlWriter.WriteEndElement();
					}
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
				}
			}
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}

		private static LiquidacionClass LecturaArchivoXML(string NombreArchivo)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources/" + NombreArchivo));
			LiquidacionClass modelPlus = new LiquidacionClass();

			XmlNode liquidacionNode = xmlDoc.SelectSingleNode("/liquidaciones/liquidacion");

			modelPlus.id_liquidacion = int.Parse(liquidacionNode.SelectSingleNode("id_liquidacion")?.InnerText);
			modelPlus.numero_liquidacion = int.Parse(liquidacionNode.SelectSingleNode("numero_liquidacion")?.InnerText);
			modelPlus.id_cliente = int.Parse(liquidacionNode.SelectSingleNode("id_cliente")?.InnerText);
			modelPlus.fecha = DateTime.Parse(liquidacionNode.SelectSingleNode("fecha")?.InnerText);
			modelPlus.total_cupones = int.Parse(liquidacionNode.SelectSingleNode("total_cupones")?.InnerText);
			modelPlus.total_comision = int.Parse(liquidacionNode.SelectSingleNode("total_comision")?.InnerText);
			modelPlus.total_conceptos = int.Parse(liquidacionNode.SelectSingleNode("total_conceptos")?.InnerText);
			modelPlus.total_liquidado = int.Parse(liquidacionNode.SelectSingleNode("total_liquidado")?.InnerText);
			modelPlus.estado = liquidacionNode.SelectSingleNode("estado")?.InnerText;

			foreach (XmlNode cuponNode in liquidacionNode.SelectNodes("cupones/cupon"))
			{
				CuponLiquidacionClass cupon = new CuponLiquidacionClass();
				cupon.id_cupon = int.Parse(cuponNode.SelectSingleNode("id_cupon")?.InnerText);
				cupon.numero_cupon = int.Parse(cuponNode.SelectSingleNode("numero_cupon")?.InnerText);
				cupon.codigo_barras = int.Parse(cuponNode.SelectSingleNode("codigo_barras")?.InnerText);
				cupon.fecha_cupon = DateTime.Parse(cuponNode.SelectSingleNode("fecha_cupon")?.InnerText);
				cupon.id_moneda = int.Parse(cuponNode.SelectSingleNode("id_moneda")?.InnerText);
				cupon.importe_cobrado = int.Parse(cuponNode.SelectSingleNode("importe_cobrado")?.InnerText);
				cupon.valor_pesos_cobrado = int.Parse(cuponNode.SelectSingleNode("valor_pesos_cobrado")?.InnerText);
				cupon.id_entidad = int.Parse(cuponNode.SelectSingleNode("id_entidad")?.InnerText);
				cupon.importe_comision = int.Parse(cuponNode.SelectSingleNode("importe_comision")?.InnerText);
				cupon.fecha_cobro = DateTime.Parse(cuponNode.SelectSingleNode("fecha_cobro")?.InnerText);

				modelPlus.cupones.Add(cupon);
			}

			foreach (XmlNode conceptoNode in liquidacionNode.SelectNodes("conceptos/concepto"))
			{
				ConceptoClass concepto = new ConceptoClass();
				concepto.id_concepto = int.Parse(conceptoNode.SelectSingleNode("id_concepto")?.InnerText);
				concepto.id_moneda_concepto = int.Parse(conceptoNode.SelectSingleNode("id_moneda_concepto")?.InnerText);
				concepto.descripcion_concepto = conceptoNode.SelectSingleNode("descripcion_concepto")?.InnerText;
				concepto.importe_concepto = int.Parse(conceptoNode.SelectSingleNode("importe_concepto")?.InnerText);
				concepto.valor_pesos_concepto = int.Parse(conceptoNode.SelectSingleNode("valor_pesos_concepto")?.InnerText);
				modelPlus.conceptos.Add(concepto);
			}

			return modelPlus;
		}
	}
}
