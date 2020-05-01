using System.Collections.Generic;

namespace Ring_Defense.Engine{
class SceneArgs
{
    public SceneArgs()
    {
        props = new Dictionary<string, object>();
    }

    private Dictionary<string, object> props;

    public void AddArg(string name, object value)
    {
        props.Add(name, value);
    }

    public object GetArg(string name)
    {
        return props[name];
    }
}
}