namespace StudentHelpCare.StudentHelpCare.ViewModel.Student
{
    public class StudentDto
    {
        public static Data.Entity.StudentEntity Map(StudentViewModal viewModel)
        {
            if (viewModel == null) { return new Data.Entity.StudentEntity(); }

            return new Data.Entity.StudentEntity()
            {
                Id= viewModel.Id,
                Name = viewModel.Name,
            };
        }

        public static StudentViewModal Map(Data.Entity.StudentEntity dataEntity)
        {
            if (dataEntity == null) { return new StudentViewModal(); }

            return new StudentViewModal()
            {
                Id = dataEntity.Id,
                Name = dataEntity.Name,
            };
        }

        public static IEnumerable<StudentViewModal> Map(IEnumerable<Data.Entity.StudentEntity> dataEntityList) 
        {
            if(dataEntityList == null) { yield break; }

            foreach(var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
