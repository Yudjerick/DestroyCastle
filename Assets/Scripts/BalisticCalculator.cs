
using UnityEngine;

public static class BallisticCalculator
{
    /// <summary>
    /// Рассчитывает вектор силы выстрела для попадания в цель.
    /// </summary>
    /// <param name="startPosition">Начальная позиция выстрела.</param>
    /// <param name="targetPosition">Позиция цели.</param>
    /// <param name="launchSpeed">Скорость выстрела.</param>
    /// <returns>Вектор выстрела или null, если цель недостижима.</returns>
    public static Vector2? CalculateLaunchVector(Vector2 startPosition, Vector2 targetPosition, float launchSpeed)
    {
        // Разница позиций
        Vector2 displacement = targetPosition - startPosition;
        float dx = displacement.x;
        float dy = displacement.y;

        // Ускорение свободного падения
        float g = Mathf.Abs(Physics2D.gravity.y);

        // Проверяем достижимость цели
        float discriminant = (launchSpeed * launchSpeed * launchSpeed * launchSpeed) - g * (g * dx * dx + 2 * dy * launchSpeed * launchSpeed);

        if (discriminant < 0)
        {
            // Цель недостижима
            return null;
        }

        // Рассчитываем квадратный корень из дискриминанта
        float root = Mathf.Sqrt(discriminant);

        // Угол для выстрела
        float angle1 = Mathf.Atan2((launchSpeed * launchSpeed + root), (g * dx));
        float angle2 = Mathf.Atan2((launchSpeed * launchSpeed - root), (g * dx));

        // Выбираем один из углов (например, наибольший для более высокой траектории)
        float selectedAngle = angle2;

        // Учитываем направление смещения
        /*if (dx < 0)
        {
            selectedAngle = -selectedAngle; // Поворот на 180 градусов для стрельбы влево
        }*/

        // Рассчитываем вектор выстрела
        Vector2 launchVector = new Vector2(
            Mathf.Cos(selectedAngle),
            Mathf.Sin(selectedAngle)
        ) * launchSpeed;
        Debug.Log(launchVector);
        return launchVector;
       
    }

}
