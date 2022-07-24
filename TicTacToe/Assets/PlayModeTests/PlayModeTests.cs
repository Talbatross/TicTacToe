using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayModeTests
{
    public class PlayModeTests
    {
        [UnityTest]
        public IEnumerator TestBootstrap()
        {
            var go = new GameObject(
                "bootstrap",
                typeof(Bootstrap),
                typeof(Canvas));
            var bootstrap = go.GetComponent<Bootstrap>();
            bootstrap.canvas = go.GetComponent<Canvas>();
            bootstrap.size = 1000;
            bootstrap.boardSize = 3;
            bootstrap.playerTypeO = PlayerType.Human;
            bootstrap.playerTypeX = PlayerType.AI;
            bootstrap.horizontal = new GameObject("hori",typeof(RectTransform));
            var square = new GameObject(
                "square",
                typeof(Square),
                typeof(Button),
                typeof(RectTransform));
            bootstrap.square = square;
            var squareComponent = square.GetComponent<Square>();
            var rawImageGo = new GameObject("rawImage", typeof(RawImage));
            squareComponent.o = new Texture2D(5, 5);
            squareComponent.x = new Texture2D(5, 5);
            squareComponent.teamImage = rawImageGo.GetComponent<RawImage>();
            bootstrap.vertical = new GameObject("vert",typeof(RectTransform));
            var button = new GameObject("button", typeof(Button));
            bootstrap.playButton = button.GetComponent<Button>();
            var text = new GameObject("text", typeof(TextMeshProUGUI));
            bootstrap.playButtonText = text.GetComponent<TextMeshProUGUI>();
            yield return null;
            Assert.That(bootstrap.IsReady(), Is.EqualTo(true));
        }
    }
}
