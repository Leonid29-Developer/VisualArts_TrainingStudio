using System;

namespace VisualArtsTrainingStudio
{/// <summary>  Класс. Хранить контактные данные  </summary>
    public class Contacts
    {
        public string ID { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }
        public DateTime Birthday { get; private set; }

        /// <summary> Конуструктор </summary>
        /// <param name="NewLastName">Фамилия</param>
        /// <param name="NewFirstName">Имя</param>
        /// <param name="NewMiddleName">Отчество</param>
        /// <param name="NewEmail">Электронная почта</param>
        /// <param name="NewTelephone">номер телефона</param>
        /// <param name="NewBirthday">дата рождения</param>
        public Contacts(string NewLastName, string NewFirstName, string NewMiddleName, string NewEmail, string NewTelephone, DateTime NewBirthday)
        { LastName = NewLastName; FirstName = NewFirstName; MiddleName = NewMiddleName; Email = NewEmail; Telephone = NewTelephone; Birthday = NewBirthday; }

        public void IDA(string NewID) => ID = NewID;
    }
    /// <summary>  Класс. Хранить данные заявки  </summary>
    public class InformationTraining
    {
        public string ID { get; private set; }
        public string Direction { get; private set; }
        public int Cost { get; private set; }
        public string StatusTraining { get; private set; }
        public string StatusPayment { get; private set; }
        public Contacts Contact_Student { get; private set; }
        public Contacts Contact_Teach { get; private set; }
        public DateTime Date { get; private set; }

        /// <summary> Конуструктор </summary>
        /// <param name="NewID">Номер заявки</param>
        /// <param name="NewDirection">Направление</param>
        /// <param name="NewCost">Стоимость</param>
        /// <param name="NewStatusTraining">Статус обучения</param>
        /// <param name="NewStatusPayment">Статус оплаты</param>
        /// <param name="NewStudent">Контактные данные ученика</param>
        /// <param name="NewTeach">Контактные данные преподавателя</param>
        /// <param name="NewDate">Дата</param>
        public InformationTraining(string NewID, string NewDirection, int NewCost, string NewStatusTraining, string NewStatusPayment, Contacts NewStudent, Contacts NewTeach, DateTime NewDate)
        { ID = NewID; Direction = NewDirection; Cost = NewCost; StatusTraining = NewStatusTraining; StatusPayment = NewStatusPayment; Contact_Student = NewStudent; Contact_Teach = NewTeach; Date = NewDate; }
    }
}
