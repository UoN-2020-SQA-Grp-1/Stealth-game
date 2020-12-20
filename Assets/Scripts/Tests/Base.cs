using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Zenject;
using Assets.Lib;
using NUnit.Framework;

public class Base : SceneTestFixture
{
    protected IInputReader inputReader;
    protected ITextDisplayer textDisplayer;

    [SetUp]
    public void LoadScene()
    {
        inputReader = Substitute.For<IInputReader>();
        StaticContext.Container.BindInstance(inputReader);
        textDisplayer = Substitute.For<ITextDisplayer>();
        StaticContext.Container.BindInstance(textDisplayer);
    }
}