<h4>Bu proje tamamen test/mülakat amaçlı yapılmıştır.</h4>

<p>İstenenler;</p>
<ul>
    <li>docker-compose ile dockerize edilmesi</li>
    <li>Identity ve permissionların tek bir yerden yönetilmesi</li>
    <li>RabbitMQ / Kafka tarzı Queue sistemlerinin kullanılması</li>
    <li>API katmanında JWT güvenlik ve ekstralar</li>
    <li>UI Tarafında API ile JWT üzerinden iletişim</li>
</ul>

<p>Notlar;</p>
<ul>
    <li>Test/mülakat amaçlı olduğundan dolayı sadece Identiy katmanı N-Layer Clean Arc yapılmıştır.</li>
    <li>IK.UI katmanında örnek olması için <b>router</b>, <b>store</b> sistemleri kullanılmıştır. </li>
    <li>IK.UI'da hızlı çıkması için eksiklikler var. Back-end tarafında temelleri var. Bazı özellikler UI'a bağlanmadı.</li>
    <li>Test/mülakat amaçlı olduğu için her şey tekrarlayan işlerden arındırılarak yapılmıştır. Tam anlamıyla kusursuz çalışan bir sistem yapmak değil, mekanizmanın nasıl işlediğini göstermektir.</li>
</ul>

<p>Farkında olunan eksikler;</p>
<ul>
    <li>Identity katmanında validatörler arttırarak CRUD işlemlerde valid işlem kontrolü yapılabiir.</li>
    <li>Inventory.API katmanına EP'ler ekleneren envanter kaydı girmesi sağlanabilir. Identity'nin JWT içinde `permissions` olarak gömdüğü permissionlardan EP güvenliği sağlanabilir.</li>
    <li>IK.UI katmanında listeleme, filtreleme eknelebilir. Identity.API tarafı `/users/getAll` EP'si ile destekliyor. Sadece UI eklenmedi.</li>
    <li>RabbitMQ ile fırlatılanlar Inventory.API katmanında tutulunca bazı kontrollere girilerek işlem yapılabilir.</li>
    <li>RabbitMQ consumerlar channel'lar üzerinden CQRS gibi çalışarak Clean Arch sağlanabilir.</li>
    <li>API katmanlarına CORS güvenliği eklenebilir.</li>
    <li>SSO'dan profil bilgisi alınabiliyor ama UI'da yok. `/profile/get` EP'si JWT token'e ait kullanıcının güncel bilgilerini Identity.DB'den getiriyor.</li>
    <li>docker-compose içine isteği karşılaması için HAProxy veya Nginx ReverseProxy yapısı yapılarak hem LoadBalance hem de API Gateway yapılabilir.</li>
</ul>

<p>Diagram;</p>
<img src="./SolutionItems/diagram.svg">