# SAGA Pattern (Coreography) with 3 Microservices / 3 Mikroservis ile SAGA Pattern (Koreografi)
<img width="100%" height="550" alt="image" src="https://github.com/user-attachments/assets/50ff2926-7b67-4af6-8a7b-372919235e20" />

## 0. Overview / Genel Bakış
<img width="32" height="32" alt="image" src="https://github.com/user-attachments/assets/5785a348-009c-4fb4-ba0c-2955d93ff652" />
<p>In this project, asynchronous communication between three microservices is implemented using RabbitMQ with an event- and command-based architecture. The system follows the <b>SAGA Choreography</b> approach, where each service reacts to events and triggers the next step without a central orchestrator. This design provides a loosely coupled, scalable, and resilient architecture. Both Publish/Subscribe (Pub-Sub) and Point-to-Point (P2P) messaging patterns are used. Events are published to notify multiple services, while commands/messages can also be sent directly to a specific service when needed.</p>
<img width="32" height="32" alt="image" src="https://github.com/user-attachments/assets/93d3d777-5f03-464f-b612-57a4584079df" />
<p>Bu projede, üç mikroservis arasında RabbitMQ üzerinden event ve command tabanlı asenkron bir iletişim sağlanmıştır. Sistem, merkezi bir orchestrator olmadan her servisin event’lere tepki vererek bir sonraki adımı tetiklediği <b>SAGA Koreografi (Choreography)</b> yaklaşımını kullanır. Bu tasarım servisler arasında gevşek bağlı, ölçeklenebilir ve dayanıklı bir mimari sunar. Projede Pub-Sub ve P2P iletişim yöntemleri birlikte kullanılmıştır. Event’ler birden fazla servisi bilgilendirmek için yayınlanırken, ihtiyaç durumunda mesajlar belirli bir servise doğrudan da iletilebilir.</p>

## 1. Technologies / Teknolojiler
<table>
  <thead>
    <tr>
      <th>Technology</th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>.NET 8 / C#</td>
      <td>Main platform used to build microservices</td>
      <td>Mikroservislerin geliştirildiği ana platform</td>
    </tr>
    <tr>
      <td>🛠 ASP.NET Core Web API</td>
      <td>Used to build RESTful APIs</td>
      <td>RESTful API’lerin oluşturulması</td>
    </tr>
    <tr>
      <td>🐇 RabbitMQ</td>
      <td>Asynchronous messaging between microservices</td>
      <td>Servisler arası asenkron mesajlaşma altyapısı</td>
    </tr>
    <tr>
      <td>🧪 MassTransit</td>
      <td>Abstraction for event &amp; command-based messaging</td>
      <td>Event &amp; Command tabanlı mesajlaşma yönetimi</td>
    </tr>
    <tr>
      <td>🐘 SQL Server</td>
      <td>Relational database for Order service and Inventory Service</td>
      <td>Order ve Inventory servislerinin ilişkisel veritabanı</td>
    </tr>
    <tr>
      <td>📦 Entity Framework Core</td>
      <td>ORM for SQL Server</td>
      <td>SQL Server için ORM</td>
    </tr>
    <tr>
      <td>📘 Swagger / OpenAPI</td>
      <td>API documentation and testing tool</td>
      <td>API dokümantasyonu ve test aracı</td>
    </tr>
  </tbody>
</table>

## 2. Message Types & Flows | Mesaj Türleri ve Akışlar
<table>
  <thead>
    <tr>
      <th>Message Type</th>
      <th>Flow</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>OrderCreatedEvent</td>
      <td>Order.API → Stock.API</td>
      <td>Yeni bir sipariş oluşturulduğunda yayınlanır ve stok rezervasyonu başlatılır.</td>
    </tr>
    <tr>
      <td>InventoryReservedEvent</td>
      <td>Inventory.API → Payment.API</td>
      <td>Stok başarıyla rezerve edilince ödeme sürecini başlatmak için yayınlanır.</td>
    </tr>
    <tr>
      <td>InventoryNotReservedEvent</td>
      <td>Inventory.API → Order.API</td>
      <td>Stok yetersizse yayınlanır ve sipariş durumu <b>Failed</b> olarak güncellenir.</td>
    </tr>
    <tr>
      <td>PaymentCompletedEvent</td>
      <td>Payment.API → Order.API</td>
      <td>Ödeme başarılı olunca yayınlanır ve sipariş durumu <b>Completed</b> yapılır.</td>
    </tr>
    <tr>
      <td>PaymentFailedEvent</td>
      <td>Payment.API → Order.API</td>
      <td>Ödeme başarısız olursa yayınlanır ve sipariş durumu <b>Failed</b> yapılır.</td>
    </tr>
    <tr>
      <td>PaymentFailedEvent</td>
      <td>Payment.API → Inventory.API</td>
      <td>Ödeme başarısız olduğunda telafi (compensation) amacıyla rezerve edilen stok geri eklenir.</td>
    </tr>
  </tbody>
</table>
