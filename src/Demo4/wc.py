from wordcloud import WordCloud
from datetime import datetime

def create_word_cloud(keywords, root_dir):
    font_path = root_dir + "Fonts/STFangSong.ttf"
    word_cloud_image_path = root_dir + "WordCloudImages/" + datetime.now().strftime("%Y%m%d%H%M%S") + ".png"
    wc = WordCloud(font_path=font_path, background_color='White').generate(keywords)
    wc.to_file(word_cloud_image_path)
    return word_cloud_image_path