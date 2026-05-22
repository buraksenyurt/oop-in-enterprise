namespace Storage.Test.Integration;

// Bu birim test sınıfında Algebra sınıfının Div isimli metodunun testlerini ele alıyoruz.

public class UnitTestHello
{
    /*
        Buradaki birim testin amacı, 10 / 2 işlemini Algebra sınıfındaki Div metodu ile yaptığımda
        geriye 5 değerinin döneceğinden emin olmak. Bu neden unit test yazdığımızın cevaplarından birisi.
    */

    [Fact]
    public void Div_TwoNumbers_ReturnsCorrectResult()
    {
        // Arrange
        var x = 10;
        var y = 2;

        // Act
        var algebra = new Algebra();
        var actual = algebra.Div(x, y);
        var expected = 5;

        // Assert
        Assert.Equal(expected, actual);
    }
    
    /*
        Bu test metodunda ise matematiksel bir gerçekliği kontrol ediyoruz.
        Sıfıra bölme işlemi tanımsızdır ama kod tarafında bunun bir Exception ile karşılanması beklenir.
        Algebra sınıfının Div metoduna payda olarak sıfır gönderdiğimizde,
        metodun DivideByZeroException fırlatması gerekir. 
    */
    [Fact]
    public void Div_Zero_Returns_DivideByZeroException()
    {
        // Arrange
        var x = 10;
        var y = 0;

        // Act
        var algebra = new Algebra();
        
        // Assert
        Assert.Throws<DivideByZeroException>(() => algebra.Div(x, y));
    }
}

public class Algebra
{
    public int Div(int a, int b)
    {
        // return a + b;
        return a / b;
    }
}

//TODO: Newton Raphson yöntemi ile karekök alma algoritması ve onun için de birim testler yazalım.
