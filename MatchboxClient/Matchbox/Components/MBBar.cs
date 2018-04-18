using System;
using System.Globalization;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Matchbox.Components
{
    public class MBBar : Component
    {
        Node barNode;
        Node textNode;
        Text3D text3D;
        Color color;
        float lastUpdateValue;

        public float Value
        {
            get { return barNode.Scale.Y; }
            set { barNode.Scale = new Vector3(1, value < 0.3f ? 0.3f : value, 1); }
        }

        public void SetValueWithAnimation(float value) => barNode.RunActionsAsync(new EaseBackOut(new ScaleTo(3f, 1, value, 1)));

        public MBBar(Color color)
        {
            this.color = color;
            ReceiveSceneUpdates = true;
        }

        public override void OnAttachedToNode(Node node)
        {
            barNode = node.CreateChild();
            barNode.Scale = new Vector3(1, 0, 1); //means zero height
            var box = barNode.CreateComponent<Box>();
            box.Color = color;

            textNode = node.CreateChild();
            textNode.Rotate(new Quaternion(0, 180, 0), TransformSpace.World);
            textNode.Position = new Vector3(0, 10, 0);
            text3D = textNode.CreateComponent<Text3D>();
            text3D.SetFont(CoreAssets.Fonts.AnonymousPro, 60);
            text3D.TextEffect = TextEffect.Stroke;

            base.OnAttachedToNode(node);
        }

        protected override void OnUpdate(float timeStep)
        {
            var pos = barNode.Position;
            var scale = barNode.Scale;
            barNode.Position = new Vector3(pos.X, scale.Y / 2f, pos.Z);
            textNode.Position = new Vector3(0.5f, scale.Y + 0.2f, 0);
            var newValue = (float)Math.Round(scale.Y, 1);
            if (lastUpdateValue != newValue)
                text3D.Text = newValue.ToString("F01", CultureInfo.InvariantCulture);
            lastUpdateValue = newValue;
        }

        public void Deselect()
        {
            barNode.RemoveAllActions();//TODO: remove only "selection" action
            barNode.RunActions(new EaseBackOut(new TintTo(1f, color.R, color.G, color.B)));
        }

        public void Select()
        {
            Selected?.Invoke(this);
            // "blinking" animation
            barNode.RunActions(new RepeatForever(new TintTo(0.3f, 1f, 1f, 1f), new TintTo(0.3f, color.R, color.G, color.B)));
        }

        public event Action<MBBar> Selected;
    }
}
