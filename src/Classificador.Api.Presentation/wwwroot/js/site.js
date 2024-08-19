function SetValueToCategoryInput(event, categoryId)
{
    event.preventDefault()

    const inputIdCategory = document.getElementById('InputIdCategory');
    inputIdCategory.value = categoryId;

    document.getElementById('PendingClassifyForm').submit();
}