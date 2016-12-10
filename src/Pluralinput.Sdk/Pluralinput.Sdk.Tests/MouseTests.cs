using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pluralinput.Sdk;
using FluentAssertions;
using FakeItEasy;
using Xunit;

using static Pluralinput.Sdk.NativeConst;

namespace Pluralinput.Sdk.Tests
{
    public class MouseTests
    {
        [Theory]
        [InlineData(RI_MOUSE_LEFT_BUTTON_UP, VirtualKeys.LeftButton)]
        [InlineData(RI_MOUSE_RIGHT_BUTTON_UP, VirtualKeys.RightButton)]
        [InlineData(RI_MOUSE_MIDDLE_BUTTON_UP, VirtualKeys.MiddleButton)]
        public void InputSourceButtonUp_ShouldRaiseMouseButtonUpEvent(ushort rawBtnFlags, VirtualKeys vk)
        {
            // Arrange
            var inputSource = A.Fake<IRawInputSource>();
            var sut = new Mouse(IntPtr.Zero, inputSource);
            sut.MonitorEvents();

            // Act
            inputSource.RaiseFakeInputSourceMouseEvent(rawBtnFlags);

            // Assert
            sut
                .ShouldRaise(nameof(sut.ButtonUp))
                .WithSender(sut)
                .WithArgs<MouseButtonInputEventArgs>(args => args.Button == vk);
        }

        [Fact]
        public void InputSourceMouseInput_WithoutMatchingDeviceHandle_ShouldNotRaiseMouseEvents()
        {
            var inputSource = A.Fake<IRawInputSource>();
            var sut = new Mouse(new IntPtr(123), inputSource);
            sut.MonitorEvents();

            inputSource.RaiseFakeInputSourceMouseEvent(RI_MOUSE_LEFT_BUTTON_UP);
            sut.ShouldNotRaise(nameof(sut.ButtonUp));

            inputSource.RaiseFakeInputSourceMouseEvent(RI_MOUSE_LEFT_BUTTON_DOWN);
            sut.ShouldNotRaise(nameof(sut.ButtonDown));
        }
    }

    static class InputSourceTestExtensions
    {
        public static void RaiseFakeInputSourceMouseEvent(this IRawInputSource self, ushort btnFlags)
        {
            self.MouseInput += Raise.With(self, new RawMouseInputEventArgs
            {
                Header = new NativeStructs.RAWINPUTHEADER { hDevice = IntPtr.Zero },
                Data = new NativeStructs.RAWMOUSE { ButtonFlags = btnFlags }
            });
        }
    }
}
