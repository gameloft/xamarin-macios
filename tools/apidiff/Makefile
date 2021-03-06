TOP = ../..

include $(TOP)/Make.config

ifdef SKIP_NEW_APIS #Requires https://github.com/mono/mono/commit/4c6a463678f3f0bfa599caeb66de72c7217fa95d
NEW_REGEX = "-n:.?"
endif

ifdef SKIP_ADDED_APIS
ADD_REGEX = "-a:.?"
endif

APIDIFF_DIR=.

MONO_API_INFO = $(MONO_IOS_SDK_DESTDIR)/ios-bcl/monotouch_tools/mono-api-info.exe
MONO_API_HTML = $(MONO_IOS_SDK_DESTDIR)/ios-bcl/monotouch_tools/mono-api-html.exe
MONO_BUILD = $(SYSTEM_MONO)

# I18N are excluded - but otherwise if should be like ../../builds/Makefile + what XI adds
# in the order to the api-diff.html merged file
MONO_ASSEMBLIES = mscorlib System System.Core System.Numerics\
	System.ComponentModel.DataAnnotations System.ComponentModel.Composition \
	System.Data System.Data.Services.Client \
	System.Json System.Runtime.Serialization System.ServiceModel System.ServiceModel.Web System.Web.Services \
	System.Xml System.Xml.Linq System.Xml.Serialization \
	System.IO.Compression System.IO.Compression.FileSystem System.Net System.Transactions System.Windows \
	Mono.Data.Sqlite Mono.Data.Tds Mono.Security

# this lists the mono-BCL assemblies (without extension) as they appear installed
# (relative to /Library/Frameworks/Xamarin.[Mac|iOS].framework/Versions/Current/lib/mono)

IOS_SRC_ASSEMBLIES     = \
	Xamarin.iOS/MonoTouch.Dialog-1 Xamarin.iOS/MonoTouch.NUnitLite Xamarin.iOS/OpenTK-1.0 Xamarin.iOS/System.Net.Http Xamarin.iOS/Xamarin.iOS
MAC_SRC_ASSEMBLIES     = \
	Xamarin.Mac/Xamarin.Mac Xamarin.Mac/OpenTK \
	4.5/Xamarin.Mac 4.5/OpenTK
WATCHOS_SRC_ASSEMBLIES = Xamarin.WatchOS/Xamarin.WatchOS Xamarin.WatchOS/MonoTouch.NUnitLite Xamarin.WatchOS/System.Net.Http
TVOS_SRC_ASSEMBLIES    = Xamarin.TVOS/Xamarin.TVOS Xamarin.TVOS/MonoTouch.Dialog-1 Xamarin.TVOS/MonoTouch.NUnitLite Xamarin.TVOS/OpenTK-1.0 Xamarin.TVOS/System.Net.Http
MACCATALYST_SRC_ASSEMBLIES = Xamarin.MacCatalyst/Xamarin.MacCatalyst Xamarin.MacCatalyst/Xamarin.iOS


IOS_ASSEMBLIES     = $(foreach file,$(MONO_ASSEMBLIES),Xamarin.iOS/$(file))    $(IOS_SRC_ASSEMBLIES)
MAC_ASSEMBLIES     = $(foreach file,$(MONO_ASSEMBLIES),Xamarin.Mac/$(file))     $(MAC_SRC_ASSEMBLIES)
WATCHOS_ASSEMBLIES = $(foreach file,$(filter-out Mono.Data.Tds Mono.Security,$(MONO_ASSEMBLIES)),Xamarin.WatchOS/$(file)) $(WATCHOS_SRC_ASSEMBLIES)
TVOS_ASSEMBLIES    = $(foreach file,$(MONO_ASSEMBLIES),Xamarin.TVOS/$(file))    $(TVOS_SRC_ASSEMBLIES)
MACCATALYST_ASSEMBLIES = $(foreach file,$(MONO_ASSEMBLIES),Xamarin.MacCatalyst/$(file))    $(MACCATALYST_SRC_ASSEMBLIES)

IOS_ARCH_ASSEMBLIES = native-32/Xamarin.iOS native-64/Xamarin.iOS
MAC_ARCH_ASSEMBLIES =                       native-64/Xamarin.Mac

APIDIFF_IGNORE = -i 'INSObjectProtocol'

$(APIDIFF_DIR)/.download-$(MONO_HASH).stamp:
	$(MAKE) -C $(TOP)/builds download
	$(Q) mkdir -p $(dir $@)
	$(Q) touch $@

$(MONO_API_INFO) $(MONO_API_HTML): $(APIDIFF_DIR)/.download-$(MONO_HASH).stamp

# create api info. Directory hierarchy is based on installed hierarchy
# (XM goes into temp/xm, and XI goes into temp/xi)

$(APIDIFF_DIR)/temp/xi/%.xml: $(IOS_DESTDIR)$(MONOTOUCH_PREFIX)/lib/mono/%.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_INFO) $< -o $@

$(APIDIFF_DIR)/temp/xm/%.xml: $(MAC_DESTDIR)$(MAC_FRAMEWORK_CURRENT_DIR)/lib/mono/%.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_INFO) -d $(MAC_DESTDIR)$(MAC_FRAMEWORK_CURRENT_DIR)/lib/mono/Xamarin.Mac $< -o $@

$(APIDIFF_DIR)/temp/xm/4.5/Xamarin.Mac.xml: $(MAC_DESTDIR)$(MAC_FRAMEWORK_CURRENT_DIR)/lib/mono/4.5/Xamarin.Mac.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_INFO) -d $(MAC_DESTDIR)$(MAC_FRAMEWORK_CURRENT_DIR)/lib/mono/4.5 $< -o $@

$(APIDIFF_DIR)/temp/native-%/Xamarin.Mac.xml: $(TOP)/src/build/mac/mobile-%/Xamarin.Mac.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_INFO) -d $(MAC_DESTDIR)$(MAC_FRAMEWORK_CURRENT_DIR)/lib/mono/Xamarin.Mac $< -o $@

$(APIDIFF_DIR)/temp/native-%/Xamarin.iOS.xml: $(TOP)/src/build/ios/native-%/Xamarin.iOS.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_INFO) $< -o $@

# create diff from api info and reference info
# note that we create an empty file (the 'touch' command)
# so that we get a file in all cases (so that we don't have 
# to run mono-api-html every time even if none of the
# dependencies changed)

$(APIDIFF_DIR)/diff/%.html: $(APIDIFF_DIR)/temp/%.xml $(APIDIFF_DIR)/references/%.xml $(MONO_API_HTML)
	$(Q) mkdir -p $(dir $@)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_HTML) $(NEW_REGEX) $(ADD_REGEX) $(APIDIFF_DIR)/references/$*.xml $(APIDIFF_DIR)/temp/$*.xml $(APIDIFF_IGNORE) $@
	$(Q) touch $@

# this is a hack to show the difference between iOS and tvOS
$(APIDIFF_DIR)/diff/ios-to-tvos.html: $(APIDIFF_DIR)/temp/xi/Xamarin.iOS/Xamarin.iOS.xml $(APIDIFF_DIR)/temp/xi/Xamarin.TVOS/Xamarin.TVOS.xml $(MONO_API_HTML)
	$(Q) mkdir -p $(dir $@)
	$(Q) sed -e 's_<assembly name="Xamarin.TVOS" version="0.0.0.0">_<assembly name="Xamarin.iOS" version="0.0.0.0">_' $(APIDIFF_DIR)/temp/xi/Xamarin.TVOS/Xamarin.TVOS.xml > $(APIDIFF_DIR)/temp/Xamarin.TVOS-as-iOS.xml
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_HTML) $< $(APIDIFF_DIR)/temp/Xamarin.TVOS-as-iOS.xml $@

# create diff files for all the assemblies per platform

$(APIDIFF_DIR)/mac-api-diff.html:     $(foreach file,$(MAC_ASSEMBLIES),$(APIDIFF_DIR)/diff/xm/$(file).html)
$(APIDIFF_DIR)/ios-api-diff.html:     $(foreach file,$(IOS_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).html)
$(APIDIFF_DIR)/watchos-api-diff.html: $(foreach file,$(WATCHOS_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).html)
$(APIDIFF_DIR)/tvos-api-diff.html:    $(foreach file,$(TVOS_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).html)
$(APIDIFF_DIR)/maccat-api-diff.html:  $(foreach file,$(MACCATALYST_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).html)

$(APIDIFF_DIR)/%-api-diff.html:
	$(Q) rm -f $@
	$(Q) touch $@-toc
	$(Q_GEN) for file in $?; do \
		if [[ "x0" != "x`stat -L -f %z $$file`" ]]; then  \
			cat $$file | sed "s_<h1>_<h1 id='$$file'>_" >> $@;	\
			echo "<br><hr>" >> $@;	\
			echo "<a href='#$$file'>`echo $$file | sed -e 's_html_dll_' -e 's_diff/xi/Xamarin.iOS/__' -e 's_diff/xi/Xamarin.WatchOS/__' -e 's_diff/xi/Xamarin.TVOS/__' -e 's_diff/xm/4.5/_\(Full profile\) _' -e 's_diff/xm/Xamarin.Mac/_\(Mobile profile\) _' -e 's_diff/xm/_\(Classic profile\) _' `</a><br/>" >> $@-toc; \
		fi; \
	done
	$(Q) if [ ! -f $@ ]; then \
		echo "No change detected" > $@;	\
	fi;
	$(Q) echo "<h1>API diff</h1>" > $@.temp
	$(Q) cat $@-toc >> $@.temp
	$(Q) echo "<br><hr>" >> $@.temp
	$(Q) cat $@ >> $@.temp
	$(Q) mv $@.temp $@
	$(Q) rm -f $@-toc

ifdef INCLUDE_MAC
API_DIFF_DEPENDENCIES += $(APIDIFF_DIR)/mac-api-diff.html
endif
ifdef INCLUDE_IOS
API_DIFF_DEPENDENCIES += $(APIDIFF_DIR)/ios-api-diff.html
ifdef INCLUDE_WATCH
API_DIFF_DEPENDENCIES += $(APIDIFF_DIR)/watchos-api-diff.html
endif
ifdef INCLUDE_TVOS
API_DIFF_DEPENDENCIES += $(APIDIFF_DIR)/tvos-api-diff.html
API_DIFF_DEPENDENCIES += $(APIDIFF_DIR)/diff/ios-to-tvos.html
endif
ifdef INCLUDE_MACCATALYST
API_DIFF_DEPENDENCIES += $(APIDIFF_DIR)/maccat-api-diff.html
endif
endif

$(APIDIFF_DIR)/api-diff.html: $(API_DIFF_DEPENDENCIES)
	$(QF_GEN) echo "<h1>API diffs</h1>" > $@
ifdef INCLUDE_IOS
	$(Q) if ! grep "No change detected" $(APIDIFF_DIR)/ios-api-diff.html >/dev/null 2>&1; then  \
		echo "<h2><a href='ios-api-diff.html'>Xamarin.iOS API diff</a></h2>" >> $@; \
	else \
		echo "<h2>Xamarin.iOS API diff is empty</h2>" >> $@; \
	fi;
ifdef INCLUDE_TVOS
	$(Q) if ! grep "No change detected" $(APIDIFF_DIR)/tvos-api-diff.html >/dev/null 2>&1; then  \
		echo "<h2><a href='tvos-api-diff.html'>Xamarin.TVOS API diff</a></h2>" >> $@; \
	else \
		echo "<h2>Xamarin.TVOS API diff is empty</h2>" >> $@; \
	fi;
endif
ifdef INCLUDE_WATCH
	$(Q) if ! grep "No change detected" $(APIDIFF_DIR)/watchos-api-diff.html >/dev/null 2>&1; then  \
		echo "<h2><a href='watchos-api-diff.html'>Xamarin.WatchOS API diff</a></h2>" >> $@; \
	else \
		echo "<h2>Xamarin.WatchOS API diff is empty</h2>" >> $@; \
	fi;
endif
ifdef INCLUDE_MACCATALYST
	$(Q) if ! grep "No change detected" $(APIDIFF_DIR)/maccat-api-diff.html >/dev/null 2>&1; then  \
		echo "<h2><a href='maccat-api-diff.html'>Xamarin.MacCatalyst API diff</a></h2>" >> $@; \
	else \
		echo "<h2>Xamarin.MacCatalyst API diff is empty</h2>" >> $@; \
	fi;
endif
endif
ifdef INCLUDE_MAC
	$(Q) if ! grep "No change detected" $(APIDIFF_DIR)/mac-api-diff.html >/dev/null 2>&1; then  \
		echo "<h2><a href='mac-api-diff.html'>Xamarin.Mac API diff</a></h2>" >> $@; \
	else \
		echo "<h2>Xamarin.Mac API diff is empty</h2>" >> $@; \
	fi;
endif

# easy-to-type helper targets.
# one rule to create all the api diffs

all-local:: $(APIDIFF_DIR)/api-diff.html

# Rules to re-create the reference infos from the curretn stable 'bundle.zip. assemblies

# split the URL in words based on the path separator, and then chose the 6th word (the hash) in the bundle zip filename
APIDIFF_HASH ?= $(word 6,$(subst /, ,$(APIDIFF_REFERENCES)))
BUNDLE_ZIP ?= $(APIDIFF_DIR)/bundle-$(APIDIFF_HASH).zip
$(BUNDLE_ZIP):
	# download to a temporary filename so interrupted downloads won't prevent re-downloads.
	$(Q) if test -f ~/Library/Caches/xamarin-macios/$(notdir $@); then \
		echo "Found a cached version of $(APIDIFF_REFERENCES)) in ~/Library/Caches/xamarin-macios/$(notdir $@)."; \
		$(CP) ~/Library/Caches/xamarin-macios/$(notdir $@) $@.tmp; \
	else \
		curl -L $(APIDIFF_REFERENCES) > $@.tmp; \
	fi
	$(Q) mv $@.tmp $@

UNZIP_STAMP=$(APIDIFF_DIR)/.unzip.$(APIDIFF_HASH).stamp
UNZIP_DIR=temp/downloads/$(APIDIFF_HASH)
$(UNZIP_STAMP): $(BUNDLE_ZIP)
	$(Q) rm -Rf $(UNZIP_DIR)
	$(Q) mkdir -p $(dir $(UNZIP_DIR))
	$(Q_GEN) unzip -q -d $(UNZIP_DIR) $(BUNDLE_ZIP)
	$(Q) touch $@

# the semi-colon at the end means an empty recipe, and is required for make to consider pattern rules
$(UNZIP_DIR)/%.dll: $(UNZIP_STAMP) ;

IOS_REFS     = $(foreach file,$(IOS_ASSEMBLIES),$(APIDIFF_DIR)/updated-references/xi/$(file).xml)
MAC_REFS     = $(foreach file,$(MAC_ASSEMBLIES),$(APIDIFF_DIR)/updated-references/xm/$(file).xml)
WATCHOS_REFS = $(foreach file,$(WATCHOS_ASSEMBLIES),$(APIDIFF_DIR)/updated-references/xi/$(file).xml)
TVOS_REFS    = $(foreach file,$(TVOS_ASSEMBLIES),$(APIDIFF_DIR)/updated-references/xi/$(file).xml)
MACCATALYST_REFS = $(foreach file,$(MACCATALYST_ASSEMBLIES),$(APIDIFF_DIR)/updated-references/xi/$(file).xml)

$(APIDIFF_DIR)/references/xi/%.xml: $(UNZIP_DIR)/%.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@) $(dir $(APIDIFF_DIR)/references/xi/$*)
	$(QF_GEN) mono --debug $(MONO_API_INFO) $< -o $(APIDIFF_DIR)/references/xi/$*.xml

$(APIDIFF_DIR)/updated-references/xi/%.xml: $(IOS_DESTDIR)$(MONOTOUCH_PREFIX)/lib/mono/%.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@) $(dir $(APIDIFF_DIR)/references/xi/$*)
	$(QF_GEN) mono --debug $(MONO_API_INFO) $< -o $(APIDIFF_DIR)/references/xi/$*.xml

$(APIDIFF_DIR)/references/xm/%.xml: $(UNZIP_DIR)/%.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@) $(dir $(APIDIFF_DIR)/references/xm/$*)
	$(QF_GEN) mono --debug $(MONO_API_INFO) $< -o $(APIDIFF_DIR)/references/xm/$*.xml

$(APIDIFF_DIR)/updated-references/xm/%.xml: $(MAC_DESTDIR)$(MAC_FRAMEWORK_CURRENT_DIR)/lib/mono/%.dll $(MONO_API_INFO)
	$(Q) mkdir -p $(dir $@) $(dir $(APIDIFF_DIR)/references/xm/$*)
	$(QF_GEN) mono --debug $(MONO_API_INFO) -d $(dir $<) $< -o $(APIDIFF_DIR)/references/xm/$*.xml

update-tvos-refs: $(TVOS_REFS)
update-watchos-refs: $(WATCHOS_REFS)
update-ios-refs: $(IOS_REFS)
update-mac-refs: $(MAC_REFS)
update-maccat-refs: $(MACCATALYST_REFS)

update-refs: $(WATCHOS_REFS) $(TVOS_REFS) $(IOS_REFS) $(MAC_REFS) $(MACCATALYST_REFS)

# targets to verify that the 32-bit and 64-bit assemblies have identical API.

verify-reference-assemblies-ios: $(APIDIFF_DIR)/temp/native-32/Xamarin.iOS.xml $(APIDIFF_DIR)/temp/native-64/Xamarin.iOS.xml
	$(Q) diff $(APIDIFF_DIR)/temp/native-32/Xamarin.iOS.xml $(APIDIFF_DIR)/temp/native-64/Xamarin.iOS.xml -u
	@echo iOS reference assemblies are identical

clean-local::
	rm -rf temp references diff *.exe* api-diff.html
	rm -rf *.dll* bundle-*.zip $(UNZIP_STAMP)
	rm -rf ios-*.md tvos-*.md watchos-*.md macos-*.md

DIRS += $(APIDIFF_DIR)/temp $(APIDIFF_DIR)/diff

# dir creation target
$(DIRS):
	$(Q) mkdir -p $@

# make will automatically consider files created in chained implicit rules as temporary files, and delete them afterwards
# defining a .SECONDARY rule will prevent that deletion.
.SECONDARY:

merger.exe: merger.cs
	$(Q) $(SYSTEM_CSC) -debug $< -out:$@

ifdef INCLUDE_IOS
ios-markdown: merger.exe $(foreach file,$(IOS_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).md)
	$(Q) $(SYSTEM_MONO) --debug merger.exe Xamarin.iOS $(CURDIR)/diff/xi/Xamarin.iOS/ ios
else
ios-markdown: ; @true
endif

ifdef INCLUDE_TVOS
tvos-markdown: merger.exe $(foreach file,$(TVOS_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).md)
	$(Q) $(SYSTEM_MONO) --debug merger.exe Xamarin.TVOS $(CURDIR)/diff/xi/Xamarin.TVOS/ tvos
else
tvos-markdown: ; @true
endif

ifdef INCLUDE_WATCH
watchos-markdown: merger.exe $(foreach file,$(WATCHOS_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).md)
	$(Q) $(SYSTEM_MONO) --debug merger.exe Xamarin.WatchOS $(CURDIR)/diff/xi/Xamarin.WatchOS/ watchos
else
watchos-markdown: ; @true
endif

ifdef INCLUDE_MACCATALYST
maccat-markdown: merger.exe $(foreach file,$(MACCATALYST_ASSEMBLIES),$(APIDIFF_DIR)/diff/xi/$(file).md)
	$(Q) $(SYSTEM_MONO) --debug merger.exe Xamarin.MacCatalyst $(CURDIR)/diff/xi/Xamarin.MacCatalyst/ maccat
else
maccat-markdown: ; @true
endif

ifdef INCLUDE_MAC
macos-markdown: merger.exe $(foreach file,$(MAC_ASSEMBLIES),$(APIDIFF_DIR)/diff/xm/$(file).md)
	$(Q) $(SYSTEM_MONO) --debug merger.exe Xamarin.Mac $(CURDIR)/diff/xm/Xamarin.Mac/ macos
else
macos-markdown: ; @true
endif

$(APIDIFF_DIR)/diff/%.md: $(APIDIFF_DIR)/temp/%.xml $(APIDIFF_DIR)/references/%.xml $(MONO_API_HTML)
	$(QF_GEN) $(MONO_BUILD) --debug $(MONO_API_HTML) $(NEW_REGEX) $(ADD_REGEX) $(APIDIFF_DIR)/references/$*.xml $(APIDIFF_DIR)/temp/$*.xml $(APIDIFF_IGNORE) --md $@

wrench-api-diff:
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xi/Xamarin.iOS"
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xi/Xamarin.WatchOS"
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xi/Xamarin.TVOS"
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xi/Xamarin.MacCatalyst"
ifdef INCLUDE_MAC
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xm"
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xm/4.5"
	@echo "@MonkeyWrench: AddDirectory: $(CURDIR)/diff/xm/Xamarin.Mac"
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/mac-api-diff.html"
endif
ifdef INCLUDE_IOS
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/ios-api-diff.html"
ifdef INCLUDE_WATCH
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/watchos-api-diff.html"
endif
ifdef INCLUDE_TVOS
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/tvos-api-diff.html"
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/diff/ios-to-tvos.html"
endif
ifdef INCLUDE_MACCATALYST
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/maccat-api-diff.html"
endif
endif
	$(Q) $(MAKE) $(UNZIP_STAMP)
	$(Q) $(MAKE) all -j8
	$(Q) $(MAKE) -j8 ios-markdown tvos-markdown watchos-markdown macos-markdown maccat-markdown
	$(Q) $(CP) api-diff.html index.html
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/index.html"
	@echo "@MonkeyWrench: AddFile: $(CURDIR)/api-diff.html"
	@# remove empty files so they're not uploaded
	@-find $(CURDIR)/diff -size 0 | xargs rm
ifdef INCLUDE_IOS
	$(MAKE) verify-reference-assemblies-ios -j8
endif

jenkins-api-diff:
	-$(MAKE) wrench-api-diff
	$(Q) mkdir -p $(JENKINS_RESULTS_DIRECTORY)/api-diff
	$(Q) $(CP) *.html $(JENKINS_RESULTS_DIRECTORY)/api-diff
	$(Q) $(CP) *.md $(JENKINS_RESULTS_DIRECTORY)/api-diff
	$(Q) $(CP) -R diff $(JENKINS_RESULTS_DIRECTORY)/api-diff
