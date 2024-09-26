# RetoTécnico
Prerequisito para una ejecución más practica:
seleccionar la solución, dar clic derecho y seleccionar la opción de Configurar proyectos de inicio, se mostrara una ventana, seleccionar la opción que dice proyectos de inicio multiple en el listado de proyectos columna de Acción elegir la opción de iniciar en ambos proyectos y dar clic en aceptar, con ello al ejecutar la solución se iniciara el proyecto de API y el portal.


El presente proyecto es para atender los requerimientos técnicos para la evaluación técnica.
Principales elementos a resaltar de esta solución web:
 
Contiene un Index y un layout que controlan el poder acceder a los módulos de usuarios, medicamentos y poder realizar la carga de un XML para su posterior generación de un PDF.

Cada modulo tiene su correspondiente API, en este caso las APIS se configuraron para que solo puedan ser accesibles al http://localhost:5155/ si la llamada es de un sitio diferente se denegara el acceso.

Para una mejor operación y escalabilidad, las validaciones de los campos se dejaron del lado de las APIs con el objetivo de que si se cambia algún texto de validación o bien se agrega una nueva restricción como puede ser una expresión regular, solo sería modificarse en el API y no en todos los sistemas o portales que la consumen.
Algo más a resaltar, que si bien esta indicado en el código, se parametrizaron las url de las APIS, usuario, contraseña y url del WS SOAP, esto para si en algún futuro se cambia a otro servidor o cambian los usuarios y contraseñas no se tenga que volver a compilar y publicar, solo se ajusta en el AppSetting.

Datos para iniciar sesión en el portal
Usuario: ejemplo
Password: ejemplo1$R
 
NOTA: para que se pueda iniciar sesión el usuario debe estar activo en la Base de datos.

 

 

 

 

 
