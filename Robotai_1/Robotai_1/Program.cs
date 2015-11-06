using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Robotai_1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var shapes = new List<Shape>();
            shapes.Add(new Square(10));
            shapes.Add(new Triangle(3, 4, 5));
            shapes.Add(new Rectangle(2, 3));
            shapes.Add(new Rhombus(7, 8));
            shapes.Add(new Trapeze(10, 6, 3));
            shapes.Add(new Circle(1));

            foreach (var shape in shapes)
            {
               Console.WriteLine(shape.ToString()); 
            }
        }
    }

    public enum ShapeType
    {
        [Display(Name = "Square")]
        Square = 1,

        [Display(Name = "Triangle")]
        Triangle = 2,

        [Display(Name = "Rectangle")]
        Rectangle = 3,

        [Display(Name = "Rhombus")]
        Rhombus = 4,

        [Display(Name = "Trapeze")]
        Trapeze = 5,

        [Display(Name = "Circle")]
        Circle = 6
    }

    public abstract class Shape
    {
        public ShapeType Type;
        abstract public double Area();
    }

    public class Square : Shape
    {
        public double a { get; set; }
        
        public override double Area()
        {
            return 4*a; 
        }

        public Square(double a)
        {
            this.a = a;
            this.Type = ShapeType.Square;
        }

        public override string ToString()
        {
            return String.Format("Shape: {2}\nA: {0}\nArea: {1}\n", a, Area(), EnumHelper.GetEnumDisplayValue<ShapeType>(Type));
        }
    }

    public class Triangle : Shape
    {
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }

        public override double Area()
        {
            return Math.Sqrt(SemiPerimeter*(SemiPerimeter - a)*(SemiPerimeter - b)*(SemiPerimeter - c)); 
        }

        public double SemiPerimeter
        {
            get { return (a + b + c)/2; }
        }

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.Type = ShapeType.Triangle;
        }

        public override string ToString()
        {
            return String.Format("Shape: {4}\nA: {0}\nB: {1}\nC: {2}\nArea: {3}\n", a, b, c, Area(), EnumHelper.GetEnumDisplayValue<ShapeType>(Type));
        }
    }

    public class Rectangle : Shape
    {
        public double a { get; set; }
        public double b { get; set; }

        public override double Area()
        {
            return a*b; 
        }

        public Rectangle(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.Type = ShapeType.Rectangle;
        }

        public override string ToString()
        {
            return String.Format("Shape: {3}\nA: {0}\nB: {1}\nArea: {2}\n", a, b, Area(), EnumHelper.GetEnumDisplayValue<ShapeType>(Type));
        }
    }

    public class Rhombus : Shape
    {
        public double d1 { get; set; }
        public double d2 { get; set; }


        public override double Area()
        {
            return d1*d2/2; 
        }

        public Rhombus(double d1, double d2)
        {
            this.d1 = d1;
            this.d2 = d2;
            this.Type = ShapeType.Rhombus;
        }

        public override string ToString()
        {
            return String.Format("Shape: {3}\nDiagonal 1: {0}\nDiagonal 2: {1}\nArea: {2}\n", d1, d2, Area(), EnumHelper.GetEnumDisplayValue<ShapeType>(Type));
        }
    }

    public class Trapeze : Shape 
    {
        public double a { get; set; }
        public double b { get; set; }
        public double h { get; set; }

        public override double Area()
        {
            return ((a + b)/2)*h; 
        }

        public Trapeze(double a, double b, double h)
        {
            this.a = a;
            this.b = b;
            this.h = h;
            this.Type = ShapeType.Trapeze;
        }

        public override string ToString()
        {
            return String.Format("Shape: {4}\nA: {0}\nB: {1}\nh: {2}\nArea: {3}\n", a, b, h, Area(), EnumHelper.GetEnumDisplayValue<ShapeType>(Type));
        }
    }

    public class Circle : Shape
    {
        public double r { get; set; }

        public override double Area()
        {
            return Math.PI*r*r;  
        }

        public Circle(double r)
        {
            this.r = r;
            this.Type = ShapeType.Circle;
        }

        public override string ToString()
        {
            return String.Format("Shape: {2}\nr: {0}\nArea: {1}\n", r, Area(), EnumHelper.GetEnumDisplayValue<ShapeType>(Type));
        }
    }

    public class EnumHelper
    {
        public static string GetEnumDisplayValue<TEnum>(object member)
        {
            if (member == null)
            {
                return string.Empty;
            }

            var type = typeof(TEnum);
            var memberName = ((TEnum)member).ToString();
            var field = type.GetField(memberName);
            var displayName = field != null ? (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) : null;

            return displayName != null ? displayName.Name : memberName;
        }
    }
}
