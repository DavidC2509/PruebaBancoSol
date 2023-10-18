

## Cambios Realizados
Para el Punto 1 de Creacion de Servicios se Modifico el contructor de Transaction a un interno y por metodos estaticos sus creaciones tanto para incomes o expenses. (Motivo por tema de no tener los Agregado Root o Child contructores que puedan ser llamada desde cualquier parte de la aplicacion sino contructores especificos de lo quieras crear. Mas ejemlos https://github.com/DavidC2509/backend-sitec)

Para el Punto 2 Se modifico TransactionFinanceService Con diferentes Servicios separado por cada obtencion de por dia o Mes o Semana y el ultimo por Fecha abierta creado. Pdd(LLevaria los servicios a Infractucture y las interfaces tal cual nomas en Core)

Para el punto 3 se Implemento Cache Redis Usando Dapr Redis para su implementacion.


Para el uso de Dapr se implemento Docker para ver las trazas de Dapr http://localhost:5411/zipkin/
