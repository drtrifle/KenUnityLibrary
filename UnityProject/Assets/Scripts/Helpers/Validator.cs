using UnityEngine;

namespace KenCode
{
    public static class Validator
    {
        public static void AssertReferenceNotNull(UnityEngine.Object objRef, string objRefName, Transform transform)
        {
            Debug.Assert(objRef != null, $"Ref:{objRefName} not set in GameObj:{transform.name} under ParentGameObj:{transform.root.name}", transform);
        }
    }
}
