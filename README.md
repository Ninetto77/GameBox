<h1>Сингх Нина Джасбировна</h1>

<h2>Описание проекта:</h2>
<p>A - движение влево</p>
<p>S - движение назад</p>
<p>D - движение вправо</p>
<p>W - движение вперед</p>
<p>B - открыть инвентарь</p>
<p>U - убрать всю экипировку</p>
<p>E - взаимодействие с печью и столом</p>
<p>ПКМ - взять предмет (при наведении мышки на предмет)</p>
<p>ПКМ в инвентаре - экипировать предмет</p>

<h2>Что можно делать:</h2>
<p>- взять камень, бревно, топор или кирку</p>
<p>- добыть киркой камень</p>
<p>- добыть топором дерево</p>
<p>- положить 2 бревна в печь</p>
<p>- положить 3 камня на стол</p>

<h2>Баг:</h2>
<p>При взятии в руку инструменты перевернуты (надо, чтобы префабы располагались на одной плоскости. Так как префабы были скачены с интернета, их меш на разных плоскостях расположены). В данном черновом варианте этот баг не имеет большой роли.</p>

<h2>Что можно исправить:</h2>
<ol>
  <li>Печь и стол имеют практически один функционал - при нажатии клавиши Е мы забираем из инвентаря определенный предмет. Можно было создать абстрактный класс, который выполнял бы общую функцию печи и стола, и дочерние от нее классы печи и стола с добавлением индивидуального функционала.</li>
    <li>Добавление перетаскивания с помощью мышки предмета из инвентаря в предмет взаимодействия (брево с печку, камень на стол)</li>
    <li>Возможно, весь ввод с клавиатуры внести в один скрипт</li>
</ol>




<h2>Что можно добавить:</h2>
<p>В связи нехватки времени не сделано улучшение инструментов. Идея реализации такая: создание скрипта, которая при выполнении определенного условия (например, наличия денег) улучшает у Instrument.cs поле Speed и Power.</p>
