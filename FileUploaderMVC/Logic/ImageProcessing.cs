namespace FileUploaderMVC.Logic {

    public class ImageProcessing {

        public static int Thresholding(int pixel,
                                       int threshold) {
            return pixel > threshold
                       ? 255
                       : 0;
        }

    }

}